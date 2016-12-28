#pragma once
#include <iostream>

namespace Silent {
	class Silent_Level{

	private:
		static int LevelCount;
	
	public:
		std::string LevelName;

		Silent_Level() {
			LevelName = "SampleLevel" + LevelCount;
			LevelCount += 1;
		}

	};
}