import { USER_LOADING, LOGIN_FAIL, LOGIN_SUCCESS } from "./types";

export const initialState = {
  token: null,
  isAuthenticated: false,
  isLoading: false,
  user: null,
  errorMessage: null,
};

export default function (state = initialState, action) {
  switch (action.type) {
    case USER_LOADING:
      return { ...state, isLoading: true };
    case LOGIN_SUCCESS:
      console.log(action);
      sessionStorage.setItem("token", action.payload.token);
      return {
        ...state,
        token: action.payload.token,
        user: action.payload.personalDetails,
        isAuthenticated: true,
        isLoading: false,
        errorMessage: null,
      };
    case LOGIN_FAIL:
      sessionStorage.removeItem("token");
      return {
        ...state,
        token: null,
        isAuthenticated: false,
        isLoading: false,
        user: null,
        errorMessage: action.payload?.error,
      };
    default:
      return state;
  }
}
