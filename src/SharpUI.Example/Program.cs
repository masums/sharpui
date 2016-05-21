﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static SharpUI.NativeMethods;

namespace SharpUI.Example
{
    public class Program
    {
        static UIWindow window;
        public static void Main(string[] args)
        {
            uiInitOptions o = new uiInitOptions();

            uiInit(ref o);

            List<UIMenu> menus = new List<UIMenu>();

            var menu = new UIMenu("File");
            var item = menu.AppendItem("Open");
            item.OnClicked += () =>
            {
                string filename = window.OpenFile();
                if (String.IsNullOrEmpty(filename))
                {
                    window.MessageBox("No file selected", "Don't be alarmed!", true);
                    return;
                }
                window.MessageBox("File selected", filename);
            };
            item = menu.AppendItem("Save");
            item.OnClicked += () =>
            {
                string filename = window.OpenFile();
                if (String.IsNullOrEmpty(filename))
                {
                    window.MessageBox("No file selected", "Don't be alarmed!", true);
                    return;
                }
                window.MessageBox("File selected (don't worry, it's still there)", filename);
            };
            item = menu.AppendQuitItem();
            //uiOnShouldQuit()
            menus.Add(menu);

            menu = new UIMenu("Edit");
            item = menu.AppendCheckItem("Checkable Item");
            menu.AppendSeparator();
            item = menu.AppendItem("Disabled Item");
            item.Disable();
            item = menu.AppendPreferencesItem();
            menus.Add(menu);

            menu = new UIMenu("Help");
            item = menu.AppendItem("Help");
            menu.AppendAboutItem();
            menus.Add(menu);

            window = new UIWindow("libui Control Gallery", 640, 480, true);

            window.Margined = true;
            window.OnClosing += () =>
            {
                window.Dispose();
                uiQuit();
                return 0;
            };

            var box = new UIBox(true);
            box.Padded = true;
            window.SetChild(box);

            var hbox = new UIBox();
            box.Padded = true;
            box.Append(hbox, true);

            var group = new UIGroup("Basic Controls");
            group.Margined = true;
            hbox.Append(group, false);

            var inner = new UIBox(true);
            inner.Padded = true;
            group.SetChild(inner);

            inner.Append(new UIButton("Button"), false);

            var entry = new UIEntry();
            entry.Text = "Entry";
            inner.Append(entry, false);

            inner.Append(new UILabel("Label"), false);

            inner.Append(new UISeparator(), false);

            inner.Append(new UIDateTimePicker(true, false), false);

            inner.Append(new UIDateTimePicker(false, true), false);

            inner.Append(new UIDateTimePicker(true, true), false);

            //inner.Append(new UIFontButton(), false);

            var inner2 = new UIBox(true);
            inner2.Padded = true;
            hbox.Append(inner2, true);

            group = new UIGroup("Numbers");
            group.Margined = true;
            inner2.Append(group, false);

            inner = new UIBox(true);
            inner.Padded = true;
            group.SetChild(inner);

            var spinbox = new UISpinbox(0, 100);
            inner.Append(spinbox, false);

            var slider = new UISlider(0, 100);
            inner.Append(slider, false);

            var progressbar = new UIProgressBar();
            inner.Append(progressbar, false);

            group = new UIGroup("Lists");
            group.Margined = true;
            inner2.Append(group, false);

            inner = new UIBox(true);
            inner.Padded = true;
            group.SetChild(inner);

            spinbox.OnChanged += () =>
            {
                slider.Value = spinbox.Value;
                progressbar.Value = (int)spinbox.Value;
            };

            slider.OnChanged += () =>
            {
                spinbox.Value = slider.Value;
                progressbar.Value = (int)slider.Value;
            };

            // combobox
            // editable combobox

            var rb = new UIRadioButtons();
            rb.Append("Radio Button 1");
            rb.Append("Radio Button 2");
            rb.Append("Radio Button 3");
            inner.Append(rb, true);

            var tab = new UITab();
            tab.Append("Page 1", new UIBox());
            tab.Append("Page 2", new UIBox());
            tab.Append("Page 3", new UIBox());
            inner2.Append(tab, true);

            GC.Collect();

            window.Show();
            uiMain();
            uiUninit();
            GC.KeepAlive(window);
            GC.KeepAlive(menus);
        }
    }
}