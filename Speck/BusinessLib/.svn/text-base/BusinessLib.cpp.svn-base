// 这是主 DLL 文件。

#include "stdafx.h"
#include "BusinessLib.h"

using namespace System::Diagnostics;

using namespace System;

//-----------------------------------------------------------
// This function enumerates all active (plugged in) audio
// rendering endpoint devices. It prints the friendly name
// and endpoint ID string of each endpoint device.
//-----------------------------------------------------------
#define EXIT_ON_ERROR(hres)  \
              if (FAILED(hres)) { goto Exit; }
#define SAFE_RELEASE(punk)  \
              if ((punk) != NULL)  \
                { (punk)->Release(); (punk) = NULL; }

const CLSID CLSID_MMDeviceEnumerator = __uuidof(MMDeviceEnumerator);
const IID IID_IMMDeviceEnumerator = __uuidof(IMMDeviceEnumerator);


//---------------------------------------------------
HRESULT BusinessLib::Headset::GetJackInfo(IMMDevice *pDevice,
                    IKsJackDescription **ppJackDesc)
{
    HRESULT hr = S_OK;

    IDeviceTopology *pDeviceTopology = NULL;
    IConnector *pConnFrom = NULL;
    IConnector *pConnTo = NULL;
    IPart *pPart = NULL;
    IKsJackDescription *pJackDesc = NULL;

    if (NULL != ppJackDesc)
    {
        *ppJackDesc = NULL;
    }
    if (NULL == pDevice || NULL == ppJackDesc)
    {
        return E_POINTER;
    }

    // Get the endpoint device's IDeviceTopology interface.
    hr = pDevice->Activate(__uuidof(IDeviceTopology), CLSCTX_ALL,
                           NULL, (void**)&pDeviceTopology);
    EXIT_ON_ERROR(hr)

    // The device topology for an endpoint device always
    // contains just one connector (connector number 0).
    hr = pDeviceTopology->GetConnector(0, &pConnFrom);
    EXIT_ON_ERROR(hr)

    // Step across the connection to the jack on the adapter.
    hr = pConnFrom->GetConnectedTo(&pConnTo);
    if (HRESULT_FROM_WIN32(ERROR_PATH_NOT_FOUND) == hr)
    {
        // The adapter device is not currently active.
        hr = E_NOINTERFACE;
    }
    EXIT_ON_ERROR(hr)

    // Get the connector's IPart interface.
    hr = pConnTo->QueryInterface(__uuidof(IPart), (void**)&pPart);
    EXIT_ON_ERROR(hr)

    // Activate the connector's IKsJackDescription interface.
    hr = pPart->Activate(CLSCTX_INPROC_SERVER,
                         __uuidof(IKsJackDescription), (void**)&pJackDesc);
    EXIT_ON_ERROR(hr)

    *ppJackDesc = pJackDesc;

Exit:
    SAFE_RELEASE(pDeviceTopology)
    SAFE_RELEASE(pConnFrom)
    SAFE_RELEASE(pConnTo)
    SAFE_RELEASE(pPart)

    return hr;

}


bool BusinessLib::Headset::GetCount()
{
	HRESULT hr = S_OK;
    IMMDeviceEnumerator *pEnumerator = NULL;
    IMMDeviceCollection *pCollection = NULL;
    IMMDevice *pEndpoint = NULL;
	IKsJackDescription *pJack = NULL;

	Trace::WriteLine("Headset::GetCount in\n");

    hr = CoCreateInstance(
           CLSID_MMDeviceEnumerator, NULL,
           CLSCTX_ALL, IID_IMMDeviceEnumerator,
           (void**)&pEnumerator);
    EXIT_ON_ERROR(hr)

    hr = pEnumerator->EnumAudioEndpoints(
                        eRender, DEVICE_STATE_ACTIVE,
                        &pCollection);
    EXIT_ON_ERROR(hr)

    UINT  count;
    hr = pCollection->GetCount(&count);
    EXIT_ON_ERROR(hr)

    if (count == 0)
    {
        Trace::WriteLine("No endpoints found.\n");
    }
	
	Trace::WriteLine("count ="+ count.ToString());
    // Each loop prints the name of an endpoint device.
    for (ULONG i = 0; i < count; i++)
    {
        // Get pointer to endpoint number i.
        hr = pCollection->Item(i, &pEndpoint);
        EXIT_ON_ERROR(hr)

		GetJackInfo(pEndpoint,&pJack);
		UINT  count1;
		KSJACK_DESCRIPTION Description1;

		pJack->GetJackCount(&count1);
		Trace::WriteLine("count1 ="+count1.ToString());

		for(ULONG j=0;j< count1;j++)
		{
		    pJack->GetJackDescription(j,&Description1);
			UINT  type1= (UINT)Description1.ConnectionType;
			Trace::WriteLine("ConnectionType ="+type1.ToString());
			if(Description1.ConnectionType == eConnType3Point5mm)
			{
			    Trace::WriteLine("IsConnected ="+Description1.IsConnected.ToString());
				if(Description1.IsConnected == true)
				{
					pJack = NULL;
                    SAFE_RELEASE(pEndpoint)
				    Trace::WriteLine("you get it!");
					return true;
				}
			}
		}

		pJack = NULL;
        SAFE_RELEASE(pEndpoint)
    }
    SAFE_RELEASE(pEnumerator)
    SAFE_RELEASE(pCollection)

Exit:
    printf("Error!\n");
    SAFE_RELEASE(pEnumerator)
    SAFE_RELEASE(pCollection)
    SAFE_RELEASE(pEndpoint)
	return false;
}