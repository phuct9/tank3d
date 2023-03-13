using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesManager : MonoBehaviour
{

    [SerializeField]
    private AssetReference assetReference;

    void Start()
    {
        Debug.Log("Khoi tao Addressable");
        Addressables.InitializeAsync().Completed += AddressablesManager_Completed;
    }

    private void AddressablesManager_Completed(AsyncOperationHandle<IResourceLocator> obj)
    {
        Debug.Log("Khoi tao xong Addressable");
        assetReference.InstantiateAsync().Completed += (go) =>
        {
            //gameObject = go.Result;
        };
        Debug.Log("Load xong Addressable");
    }

    void Update()
    {
        
    }

}
