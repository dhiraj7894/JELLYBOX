using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Jelly.Core.MP
{
    public class NetworkRunnerHandler : MonoBehaviour
    {
        public NetworkRunner networkRunnerPrefab;

        NetworkRunner networkRunner;
        private void Start()
        {
            networkRunner = Instantiate(networkRunnerPrefab);
            networkRunner.name = "Network Runner";
            var clientTask = InitializeNetworkRunner(networkRunner,
                                                     GameMode.AutoHostOrClient,
                                                     NetAddress.Any(),
                                                     SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex),
                                                     null);
        }



        protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode mode, NetAddress address, SceneRef scene, Action<NetworkRunner> initialized)
        {
            var sceneManager = runner .GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();
            if(sceneManager == null)
            {
                sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
            }
            runner.ProvideInput = true;
            return runner.StartGame(
                new StartGameArgs
                {
                    GameMode = mode,
                    Address = address,
                    Scene = scene,
                    SessionName = "TestRoom",                    
                    SceneManager = sceneManager
                }
                );
        }
    }
}
