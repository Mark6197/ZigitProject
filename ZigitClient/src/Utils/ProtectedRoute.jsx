import React, { useEffect } from "react";
import { Navigate, Outlet } from "react-router-dom";
import { useAuthState } from "../Context/Auth";
const ProtectedRoute = () => {
  const { token, isAuthenticated } = useAuthState();

  useEffect(() => {
    console.log(token, isAuthenticated);
  }, [token, isAuthenticated]);

  if (!token || !isAuthenticated) {
    return <Navigate to="/" replace />;
  }

  return <Outlet />;
};

export default ProtectedRoute;
