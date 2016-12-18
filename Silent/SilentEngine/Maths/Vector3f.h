#pragma once

namespace Silent {
	namespace Math {
		class Vector3f {
			float X, Y, Z;
		public:
			Vector3f(int X,int Y,int Z) {
				this->X = X;
				this->Y = Y;
				this->Z = Z;
			}

			Vector3f operator+=(Vector3f* toAdd) {
				this->X += toAdd->X;
				this->Y += toAdd->Y;
				this->Z += toAdd->Z;
				return *this;
			}

			Vector3f operator-=(Vector3f* toSubtract) {
				this->X -= toSubtract->X;
				this->Y -= toSubtract->Y;
				this->Z -= toSubtract->Z;
				return *this;
			}

		};
} }