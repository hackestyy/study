// BusinessLib.h
#include <windows.h>
#include <Winerror.h>
#include <Mmdeviceapi.h>
#include <Functiondiscoverykeys_devpkey.h>
#include <Objbase.h>
#include <string>
#include <Devicetopology.h> 


#pragma once

namespace BusinessLib {

	public ref class Headset
	{
	    private:
			HRESULT GetJackInfo(IMMDevice *pDevice,IKsJackDescription **ppJackDesc);

	    public:
	        bool GetCount();
	};
}
