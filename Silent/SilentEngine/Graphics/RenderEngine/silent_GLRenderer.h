#pragma once
#include "glew.h"

namespace Silent {
	namespace Graphics {
		class silent_GLRenderer {
		public:
			void PrepareToRender();
			void Render();
		};
} }