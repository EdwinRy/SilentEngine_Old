#pragma once

namespace Silent {
	namespace Entities {
		class silent_Entity {
		public:
			bool Active = true;
			bool Visible = true;

			char* EntityName = "SampleEntity";

			char* EntityModelPath = "EngineAssets/SampleEntityModel.obj";
			char* EntityTexturePath = "EngineAssets/SampleEntityTexture.png";


		};
	}
}