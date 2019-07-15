﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Microsoft.MixedReality.SpectatorView
{
    public class DeviceInfoObserver : MonoBehaviour
    {
        private static readonly TimeSpan trackingStalledReceiveDelay = TimeSpan.FromSeconds(1.0);

        public const string CreateSharedSpatialCoordinateCommand = "CreateSharedSpatialCoordinate";
        public const string DeviceInfoCommand = "DeviceInfo";
        public const string StatusCommand = "Status";
        public const float arUcoMarkerSizeInMeters = 0.1f;

        private INetworkManager networkManager;
        private SocketEndpoint connectedEndpoint;
        private string deviceName;
        private string deviceIPAddress;

        /// <summary>
        /// Gets the network manager associated with the device.
        /// </summary>
        public INetworkManager NetworkManager => networkManager;

        /// <summary>
        /// Gets the SocketEndpoint for the currently-connected device.
        /// </summary>
        public SocketEndpoint ConnectedEndpoint => connectedEndpoint;

        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        public string DeviceName => deviceName;

        /// <summary>
        /// Gets the IP address reported by the device itself.
        /// </summary>
        public string DeviceIPAddress => deviceIPAddress;

        /// <summary>
        /// Gets whether or not the receipt of new poses from the device has stalled for an unexpectedly-large time.
        /// </summary>
        public bool IsTrackingStalled => networkManager.IsConnected && networkManager.TimeSinceLastUpdate > trackingStalledReceiveDelay;

        private void Awake()
        {
            networkManager = GetComponent<INetworkManager>();
            networkManager.Connected += OnConnected;
            networkManager.Disconnected += OnDisconnected;
            networkManager.RegisterCommandHandler(DeviceInfoCommand, HandleDeviceInfoCommand);
        }

        private void OnDestroy()
        {
            if (networkManager != null)
            {
                networkManager.Connected -= OnConnected;
                networkManager.Disconnected -= OnDisconnected;
                networkManager.UnregisterCommandHandler(DeviceInfoCommand, HandleDeviceInfoCommand);
            }
        }

        private void OnConnected(SocketEndpoint endpoint)
        {
            connectedEndpoint = endpoint;
        }

        private void OnDisconnected(SocketEndpoint endpoint)
        {
            if (connectedEndpoint == endpoint)
            {
                connectedEndpoint = null;
            }
        }

        private void HandleDeviceInfoCommand(SocketEndpoint endpoint, string command, BinaryReader reader, int remainingDataSize)
        {
            deviceName = reader.ReadString();
            deviceIPAddress = reader.ReadString();
        }
    }
}