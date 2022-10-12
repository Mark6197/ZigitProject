import React, { useContext, createContext, useReducer, useEffect } from "react";
import AuthReducer, { initialState } from "./reducer";
// import { loadUser } from "./index";

// This context object will contain the authentication token and user details
const AuthStateContext = createContext();

// We will use this context object to pass the dispatch method given to us by the useReducer
// This makes it easy to provide the dispatch method to components that need it
const AuthDispatchContext = createContext();

// Custom hooks that will help us read values from these context objects without having to call React.useContext
// in every component that needs the context value and it also does some error handling in case we try to reach
// the context outside of the context provider

export function useAuthState() {
  const context = useContext(AuthStateContext);
  if (context == undefined) {
    throw new Error("useAuthState must be used within a AuthProvider");
  }
  return context;
}

export function useAuthDispatch() {
  const context = useContext(AuthDispatchContext);
  if (context == undefined) {
    throw new Error("useAuthState must be used within a AuthProvider");
  }
  return context;
}
//----------------------------------------------------------------------------------------------------------------

export const AuthProvider = ({ children }) => {
  const [user, dispatch] = useReducer(AuthReducer, initialState);

  //Possible to do- add a route in backend and send the token to check if user already authenticated and get back his detials
  //this will allow auth state being saved during the session, meaning refresh won't cause us to login again

  return (
    <AuthStateContext.Provider value={user}>
      <AuthDispatchContext.Provider value={dispatch}>
        {children}
      </AuthDispatchContext.Provider>
    </AuthStateContext.Provider>
  );
};
