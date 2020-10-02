#region license

// Copyright (C) 2020 ClassicUO Development Community on Github
// 
// This project is an alternative client for the game Ultima Online.
// The goal of this is to develop a lightweight client considering
// new technologies.
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <https://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Generic;
using ClassicUO.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClassicUO.Game.UI.Controls
{
    internal class CheckerTrans : Control
    {
        //TODO(deccer): should be moved into Renderer namespace
        private static readonly Lazy<DepthStencilState> _checkerStencil = new Lazy<DepthStencilState>
        (
            () =>
            {
                DepthStencilState depthStencilState = new DepthStencilState
                {
                    StencilEnable = true,
                    StencilFunction = CompareFunction.Always,
                    StencilPass = StencilOperation.Replace,
                    DepthBufferFunction = CompareFunction.Always,
                    ReferenceStencil = 0,
                    DepthBufferEnable = true,
                    DepthBufferWriteEnable = true
                };


                return depthStencilState;
            }
        );


        //TODO(deccer): should be moved into Renderer namespace
        private static readonly Lazy<BlendState> _checkerBlend = new Lazy<BlendState>
        (
            () =>
            {
                BlendState blendState = new BlendState
                {
                    ColorWriteChannels = ColorWriteChannels.None
                };

                return blendState;
            }
        );

        //public CheckerTrans(float alpha = 0.5f)
        //{
        //    _alpha = alpha;
        //    AcceptMouseInput = false;
        //}

        public CheckerTrans(List<string> parts)
        {
            X = int.Parse(parts[1]);
            Y = int.Parse(parts[2]);
            Width = int.Parse(parts[3]);
            Height = int.Parse(parts[4]);
            AcceptMouseInput = false;
            IsFromServer = true;
        }


        public override bool Draw(UltimaBatcher2D batcher, int x, int y)
        {
            ResetHueVector();
            _hueVector.Z = 0.5f;

            batcher.SetStencil(_checkerStencil.Value);
            batcher.Draw2D(Texture2DCache.GetTexture(Color.Black), x, y, Width, Height, ref _hueVector);
            batcher.SetStencil(null);

            return true;
        }
    }
}