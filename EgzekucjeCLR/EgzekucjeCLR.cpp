#include "stdafx.h"
using namespace System;

extern "C"
{

#pragma region Upomnienia
	__declspec(dllexport) wchar_t* InicjujBiblioteke(long )
	{
		static CStringW output("OK");
		try
		{
			Egzekucje::NET::Adapters::EgzekucjeAdapterJson::Initialize();
		}
		catch (Exception^ exc)
		{
			output = exc->Message;

		}
		return output.GetBuffer(0);
	}
	__declspec(dllexport) wchar_t* PobierzListeZaleglosci(long listaZaleglosci_)
	{
		static CStringW output;
		try
		{
			output = Egzekucje::NET::Adapters::EgzekucjeAdapterJson::PobierzListeZaleglosci(listaZaleglosci_);
		}
		catch (Exception^ exc)
		{
			output = exc->Message;
		}
		return output.GetBuffer(0);
	}
	__declspec(dllexport) wchar_t* PobierzSumeZaleglosciOsobNaDzienBiezacy()
	{
		static CStringW output;
		try
		{
			output = Egzekucje::NET::Adapters::EgzekucjeAdapterJson::PobierzSumyZaleglosciOsobNaDzienBiezacy();
		}
		catch (Exception^ exc)
		{
			output = exc->Message;
		}
		return output.GetBuffer(0);
	}
	__declspec(dllexport) wchar_t* PrzeksztalcNaleznosciNaZaleglosciNaDzienBiezacy()
	{
		static CStringW output;
		try
		{
			output = Egzekucje::NET::Adapters::EgzekucjeAdapterJson::PrzeksztalcNaleznosciNaZaleglosciNaDzienBiezacy();
		}
		catch (Exception^ exc)
		{
			output = exc->Message;
		}
		return output.GetBuffer(0);
	}
	__declspec(dllexport) wchar_t* WystawUpomnienie(wchar_t* listaZaleglosci_)
	{
		static CStringW output;
		try
		{
			String^ listaZaleglosci = gcnew String(CString(listaZaleglosci_));
			output = Egzekucje::NET::Adapters::EgzekucjeAdapterJson::WystawUpomnienie(listaZaleglosci);
		}
		catch (Exception^ exc)
		{
			output = exc->Message;
		}
		return output.GetBuffer(0);

	}
	__declspec(dllexport) wchar_t* PobierzDokumentUpomnienia(long idUpomnienia)
	{
		static CStringW output;
		try
		{
			output = Egzekucje::NET::Adapters::EgzekucjeAdapterJson::PobierzDokumentUpomnienia(idUpomnienia);
		}
		catch (Exception^ exc)
		{
			output = exc->Message;
		}
		return output.GetBuffer(0);

	}
	__declspec(dllexport) wchar_t*  PobierzAdresyAdresata(long idOsoby)
	{
		static CStringW output;
		try
		{
			output = Egzekucje::NET::Adapters::EgzekucjeAdapterJson::PobierzAdresyAdresata(idOsoby);
		}
		catch (Exception^ exc)
		{
			output = exc->Message;
		}
		return output.GetBuffer(0);

	}
	__declspec(dllexport) wchar_t* PobierzDaneAdresata(long idOsoby, long idAdresu)
	{
		static CStringW output;
		try
		{
			output = Egzekucje::NET::Adapters::EgzekucjeAdapterJson::PobierzDaneAdresata(idOsoby, idAdresu);
		}
		catch (Exception^ exc)
		{
			output = exc->Message;
		}
		return output.GetBuffer(0);

	}
	__declspec(dllexport) wchar_t* PobierzListeKontDlaAdresu(long idAdresu)
	{
		static CStringW output;
		try
		{
			output = Egzekucje::NET::Adapters::EgzekucjeAdapterJson::PobierzListeKontDlaAdresu(idAdresu);
		}
		catch (Exception^ exc)
		{
			output = exc->Message;
		}
		return output.GetBuffer(0);

	}
	__declspec(dllexport) wchar_t* TestujWczytywanieMetody(wchar_t* testText)
	{
		static CStringW output;
		try
		{
			output = testText;
		}
		catch (Exception^ exc)
		{
			output = exc->Message;
		}
		return output.GetBuffer(0);

	}
#pragma endregion
}
