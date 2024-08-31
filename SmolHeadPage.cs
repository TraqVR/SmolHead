using BananaOS;
using BananaOS.Pages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Unity.Mathematics;
using UnityEngine;

namespace SmolHead
{
    public class SmolHeadPage : WatchPage
    {
        public override string Title => "Smol Head";
        public float Size = 0.75f;
        public override bool DisplayOnMainMenu => true;
        public GameObject HeadBone;
        public override void OnPostModSetup()
        {
            selectionHandler.maxIndex = 2;
            HeadBone = GorillaTagger.Instance.offlineVRRig.transform.Find("rig/body/head").gameObject;
        }
        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<color=yellow>==</color> Smol Head <color=yellow>==</color>");
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "Enable"));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(1, "Disable"));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(2, "Size: " + Size.ToString()));
            return stringBuilder.ToString();
        }
        public void Left()
        {
            int index = selectionHandler.currentIndex;

            if (index == 2)
            {
                Size -= 0.1f;
            }
        }
        public void Right()
        {
            int index = selectionHandler.currentIndex;

            if (index == 2)
            {
                Size += 0.1f;
            }
        }

        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            switch (buttonType)
            {
                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;

                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;

                case WatchButtonType.Left:
                    Left();
                    break;

                case WatchButtonType.Right:
                    Right();
                    break;

                case WatchButtonType.Enter:
                    if (selectionHandler.currentIndex == 0)
                    {
                        HeadBone.transform.localScale = new Vector3(Size, Size, Size);
                        return;
                    }
                    HeadBone.transform.localScale = Vector3.one;
                    break;
                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;
            }
        }
    }
}