import { USER_LOADING, LOGIN_SUCCESS, LOGIN_FAIL } from "./types";
import axios from "axios";

// Login user
export const login = (dispatch, payload) => {
  dispatch({ type: USER_LOADING });
  // Headers
  const config = {
    headers: { "Content-Type": "application/json" },
  };

  const body = JSON.stringify(payload);

  axios
    .post(`${process.env.REACT_APP_ROOT_URL}/auth/login`, body, config)
    .then((res) => {
      console.log(res);
      dispatch({ type: LOGIN_SUCCESS, payload: res.data });
    })
    .catch((err) => {
      console.log(err);
      if (err.code == "ERR_NETWORK")
        dispatch({ type: LOGIN_FAIL, payload: { error: "Server error" } });
      else
        dispatch({
          type: LOGIN_FAIL,
          payload: { error: "Invalid credential" },
        });
    });
};

export const tokenConfig = (payload) => {
  //Get token from payload
  const token = payload.token;

  //Headers
  const config = {
    headers: {
      "Context-Type": "application/json",
    },
  };

  //If token exist, add to headers config
  if (token) {
    config.headers["Authorization"] = `Bearer ${token}`;
  }

  return config;
};
