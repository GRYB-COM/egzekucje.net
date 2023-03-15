// EgzekucjeConsolTest.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <wtypes.h>

typedef wchar_t* (*wystawUpomnienie)(const wchar_t*);

int main()
{
	HINSTANCE hInstLibrary;
	wystawUpomnienie _myStworzPDFFunc;

	hInstLibrary = LoadLibrary(L"EgzekucjeCLR.dll");
	if (hInstLibrary)
	{
		_myStworzPDFFunc = (wystawUpomnienie)GetProcAddress(hInstLibrary, "WystawUpomnienie");
		if (!_myStworzPDFFunc)
		{
			//MessageBox::Show("B³¹d nie zarejestrowala sie metoda wystawUpomnienie");
			FreeLibrary(hInstLibrary);
			return 0;

		}
		wchar_t* wstr = new wchar_t[2000];
		const wchar_t* json = L"{\"IdOsoby\":1009,\"IdAdresu\":1009,\"Zaleglosci\":[],\"DataUpomnienia\":\"2020-05-12T16:01:42.4355967+02:00\"}";
		wstr = _myStworzPDFFunc(json);
		int i_str;
		i_str = 1;
	}
	else
	{
		//MessageBox::Show("Brak biblioteki CPlusPlusToCSharp.dll, b¹dŸ plik jest uszkodzony.");
	}
	FreeLibrary(hInstLibrary);
	return 0;
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
