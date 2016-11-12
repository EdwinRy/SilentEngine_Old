#pragma once

namespace Silent_Core {
	namespace GameSystem {
		public ref class GL_Game
		{
		private:
			char* title;

			int m_windowWidth;
			int m_windowHeight;
			int m_windowPosX;
			int m_windowPosY;

		public:
			GL_Game();

			virtual void OnLoadGame() = 0;
			virtual void OnUpdateGame() = 0;
			virtual void OnRenderGame() = 0;
			virtual void OnClosingGame() = 0;
			virtual void OnClosedGame() = 0;

		private:

			void OnLoad();
			void OnUpdate();
			void OnRender();
			void OnClosing();
			void OnClosed();


		};
	}
}