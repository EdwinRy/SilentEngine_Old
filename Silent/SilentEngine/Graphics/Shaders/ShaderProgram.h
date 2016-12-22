#pragma once
#include"../../Maths/Vector3f.h"

namespace Silent {
	namespace Graphics {
		class ShaderProgram {
		private:
			int m_shaderID;
			int	m_vertexShaderID;
			int m_fragmentShaderID;

		public:
			ShaderProgram(char* vertexShaderFile, char* fragmentShaderFile);

			void LoadToFloat(int location, float* value);
			void LoadToVector3f(int location, Silent::Math::Vector3f*);
			void LoadToMatrix(int location, Silent::M)

		};
} }