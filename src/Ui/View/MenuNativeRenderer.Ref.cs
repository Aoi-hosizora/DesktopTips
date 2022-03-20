using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace LanDataTransmitter.Frm.View {

    public class MenuNativeRenderer : ToolStripSystemRenderer {

        private VisualStyleRenderer _renderer;
        private const string MenuClass = "Menu";

        #region Helpers

        private bool EnsureRenderer() {
            if (!VisualStyleRenderer.IsSupported) {
                return false;
            }
            if (!VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement(MenuClass, (int) MenuParts.BarBackground, (int) MenuBarStates.Active))) {
                return false;
            }
            _renderer ??= new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal);
            return true;
        }

        private static int GetItemState(ToolStripItem item) {
            var hot = item.Selected;
            if (item.IsOnDropDown) {
                if (item.Enabled) {
                    return hot ? (int) MenuPopupItemStates.Hover : (int) MenuPopupItemStates.Normal;
                }
                return hot ? (int) MenuPopupItemStates.DisabledHover : (int) MenuPopupItemStates.Disabled;
            }
            if (item.Pressed) {
                return item.Enabled ? (int) MenuBarItemStates.Pushed : (int) MenuBarItemStates.DisabledPushed;
            }
            if (item.Enabled) {
                return hot ? (int) MenuBarItemStates.Hover : (int) MenuBarItemStates.Normal;
            }
            return hot ? (int) MenuBarItemStates.DisabledHover : (int) MenuBarItemStates.Disabled;
        }

        private static Rectangle GetBackgroundRectangle(ToolStripItem item) {
            if (!item.IsOnDropDown) {
                return new Rectangle(new Point(), item.Bounds.Size);
            }
            var rect = item.Bounds;
            rect.X = item.ContentRectangle.X + 1;
            rect.Width = item.ContentRectangle.Width - 1;
            rect.Y = 0;
            return rect;
        }

        private Padding GetThemeMargins(IDeviceContext dc, MarginTypes marginType) {
            try {
                var ok = NativeMethods.GetThemeMargins(_renderer.Handle, dc.GetHdc(), _renderer.Part, _renderer.State, (int) marginType, IntPtr.Zero, out var margins) == 0;
                return !ok ? new Padding(0) : new Padding(margins.cxLeftWidth, margins.cyTopHeight, margins.cxRightWidth, margins.cyBottomHeight);
            } finally {
                dc.ReleaseHdc();
            }
        }

        #endregion

        #region Overrides

        protected override void Initialize(ToolStrip toolStrip) {
            if (toolStrip.Parent is ToolStripPanel) {
                toolStrip.BackColor = Color.Transparent;
            }
            base.Initialize(toolStrip);
        }

        protected override void InitializePanel(ToolStripPanel toolStripPanel) {
            foreach (Control control in toolStripPanel.Controls) {
                if (control is ToolStrip s) {
                    Initialize(s);
                }
            }
            base.InitializePanel(toolStripPanel);
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e) {
            if (EnsureRenderer()) {
                var partId = e.Item.IsOnDropDown ? (int) MenuParts.PopupItem : (int) MenuParts.BarItem;
                _renderer.SetParameters(MenuClass, partId, GetItemState(e.Item));
                e.TextColor = _renderer.GetColor(ColorProperty.TextColor);
            }
            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e) {
            if (EnsureRenderer()) {
                var partId = e.Item.IsOnDropDown ? (int) MenuParts.PopupItem : (int) MenuParts.BarItem;
                _renderer.SetParameters(MenuClass, partId, GetItemState(e.Item));
                e.ArrowColor = _renderer.GetColor(ColorProperty.TextColor);
            }
            base.OnRenderArrow(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e) {
            if (!EnsureRenderer() || !e.ToolStrip.IsDropDown) {
                base.OnRenderSeparator(e);
                return;
            }
            _renderer.SetParameters(MenuClass, (int) MenuParts.PopupSeparator, 0);
            var rect = new Rectangle(e.ToolStrip.DisplayRectangle.Left, 0, e.ToolStrip.DisplayRectangle.Width, e.Item.Height);
            _renderer.DrawBackground(e.Graphics, rect, rect);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e) {
            if (!EnsureRenderer()) {
                base.OnRenderMenuItemBackground(e);
                return;
            }
            var partId = e.Item.IsOnDropDown ? (int) MenuParts.PopupItem : (int) MenuParts.BarItem;
            _renderer.SetParameters(MenuClass, partId, GetItemState(e.Item));
            var bgRect = GetBackgroundRectangle(e.Item);
            _renderer.DrawBackground(e.Graphics, bgRect, bgRect);
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e) {
            if (!EnsureRenderer()) {
                base.OnRenderImageMargin(e);
                return;
            }
            if (!e.ToolStrip.IsDropDown) {
                return;
            }
            _renderer.SetParameters(MenuClass, (int) MenuParts.PopupGutter, 0);
            var margins = GetThemeMargins(e.Graphics, MarginTypes.Sizing);
            var extraWidth = e.ToolStrip.Width - e.ToolStrip.DisplayRectangle.Width - margins.Left - margins.Right - 1 - e.AffectedBounds.Width;
            var rect = e.AffectedBounds;
            rect.Y += 2;
            rect.Height -= 4;
            var sepWidth = _renderer.GetPartSize(e.Graphics, ThemeSizeType.True).Width;
            if (e.ToolStrip.RightToLeft == RightToLeft.Yes) {
                rect = new Rectangle(rect.X - extraWidth, rect.Y, sepWidth, rect.Height);
                rect.X += sepWidth;
            } else {
                rect = new Rectangle(rect.Width + extraWidth - sepWidth, rect.Y, sepWidth, rect.Height);
            }
            _renderer.DrawBackground(e.Graphics, rect);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e) {
            if (!EnsureRenderer()) {
                base.OnRenderItemCheck(e);
                return;
            }
            var bgRect = GetBackgroundRectangle(e.Item);
            bgRect.Width = bgRect.Height;
            if (e.Item.RightToLeft == RightToLeft.Yes) {
                bgRect = new Rectangle(e.ToolStrip.ClientSize.Width - bgRect.X - bgRect.Width, bgRect.Y, bgRect.Width, bgRect.Height);
            }
            _renderer.SetParameters(MenuClass, (int) MenuParts.PopupCheckBackground, e.Item.Enabled ? (int) MenuPopupCheckBackgroundStates.Normal : (int) MenuPopupCheckBackgroundStates.Disabled);
            _renderer.DrawBackground(e.Graphics, bgRect);
            var checkRect = e.ImageRectangle;
            checkRect.X = bgRect.X + bgRect.Width / 2 - checkRect.Width / 2;
            checkRect.Y = bgRect.Y + bgRect.Height / 2 - checkRect.Height / 2;
            _renderer.SetParameters(MenuClass, (int) MenuParts.PopupCheck, e.Item.Enabled ? (int) MenuPopupCheckStates.CheckmarkNormal : (int) MenuPopupCheckStates.CheckmarkDisabled);
            _renderer.DrawBackground(e.Graphics, checkRect);
        }

        #endregion

        #region Enums

        private enum MenuParts {
            BarBackground = 7,
            BarItem = 8,
            PopupBackground = 9,
            PopupBorders = 10,
            PopupCheck = 11,
            PopupCheckBackground = 12,
            PopupGutter = 13,
            PopupItem = 14,
            PopupSeparator = 15,
            PopupSubmenu = 16,
        }

        private enum MenuBarStates {
            Active = 1,
            Inactive = 2
        }

        private enum MenuBarItemStates {
            Normal = 1,
            Hover = 2,
            Pushed = 3,
            Disabled = 4,
            DisabledHover = 5,
            DisabledPushed = 6
        }

        private enum MenuPopupItemStates {
            Normal = 1,
            Hover = 2,
            Disabled = 3,
            DisabledHover = 4
        }

        private enum MenuPopupCheckStates {
            CheckmarkNormal = 1,
            CheckmarkDisabled = 2,
            BulletNormal = 3,
            BulletDisabled = 4
        }

        private enum MenuPopupCheckBackgroundStates {
            Disabled = 1,
            Normal = 2,
            Bitmap = 3
        }

        private enum MarginTypes {
            Sizing = 3601,
            Content = 3602,
            Caption = 3603
        }

        #endregion

        internal static class NativeMethods {
            [SuppressMessage("ReSharper", "InconsistentNaming")]
            [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
            [StructLayout(LayoutKind.Sequential)]
            public struct MARGINS {
                public int cxLeftWidth;
                public int cxRightWidth;
                public int cyTopHeight;
                public int cyBottomHeight;
            }

            [DllImport("uxtheme.dll")]
            public static extern int GetThemeMargins(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, int iPropId, IntPtr rect, out MARGINS pMargins);
        }
    }
}
