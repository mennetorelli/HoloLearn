﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

// Enable this preprocessor directive in your player settings as needed.
#if COMPOSITOR_PLUGIN_AVAILABLE
using SpectatorView;
#endif

using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Extensions.Experimental.SpectatorView
{
    internal class CompositorWrapper
    {
        public static RenderTexture GetDSLRFeed()
        {
#if COMPOSITOR_PLUGIN_AVAILABLE
            // Obtain DSLR Feed
            return ShaderManager.Instance.compositeTexture; // Note: figure out what is the correct texture to use here in the new compositor wrapper
#else
            return null;
#endif
        }

        public static Texture2D GetDSLRTexture()
        {
#if COMPOSITOR_PLUGIN_AVAILABLE
            // Obtain DSLR Image
            Texture2D dslrTexture = new Texture2D(
                ShaderManager.Instance.colorRGBTexture.width,
                ShaderManager.Instance.colorRGBTexture.height,
                TextureFormat.RGB24,
                false);

            var previousActive = RenderTexture.active;
            RenderTexture.active = ShaderManager.Instance.colorRGBTexture;
            dslrTexture.ReadPixels(new Rect(0, 0, dslrTexture.width, dslrTexture.height), 0, 0);
            dslrTexture.Apply();
            RenderTexture.active = previousActive;

            return dslrTexture;
#else
            return null;
#endif
        }
    }
}