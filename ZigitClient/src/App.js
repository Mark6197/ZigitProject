import "./App.css";
import LoginPage from "./Pages/Login/LoginPage";
import ProjectsPage from "./Pages/Projects/ProjectsPage";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { AuthProvider } from "./Context/Auth";
import ProtectedRoute from "./Utils/ProtectedRoute";

function App() {
  return (
    <div className="main">
      <AuthProvider>
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<LoginPage />} />
            <Route element={<ProtectedRoute />}>
              <Route path="/info" element={<ProjectsPage />} />
            </Route>
            <Route
              path="*"
              element={
                <div>
                  <h2>404 Page not found</h2>
                </div>
              }
            />
          </Routes>
        </BrowserRouter>
      </AuthProvider>
    </div>
  );
}

export default App;
