import React from "react";
import { FormHelperText, Typography } from "@mui/material";
import { LoadingButton } from "@mui/lab";
import { useForm } from "react-hook-form";
import ControlledTextField from "../../Components/ControlledInputs/ControlledTextField";
import { login, useAuthDispatch, useAuthState } from "../../Context/Auth";
import "./LoginPage.css";
import zigitLogo from "../../Resources/Images/Zigit.jpg";
import { Navigate, useNavigate } from "react-router-dom";

const LoginPage = () => {
  const dispatch = useAuthDispatch();
  const { isLoading, isAuthenticated, errorMessage } = useAuthState();
  let navigate = useNavigate();

  const onSubmit = (data, e) => {
    e.preventDefault();
    login(dispatch, {
      username: data.email,
      password: data.password,
    });

    if (isAuthenticated) navigate("/info");
  };

  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm({
    defaultValues: {
      email: "",
      password: "",
    },
  });

  return isAuthenticated ? (
    <Navigate to="/info" />
  ) : (
    <div className="loginScreenWrapper">
      <div className="loginScreenColor loginScreenColorMarked"></div>
      <div className="loginScreenColor"></div>
      <div className="loginScreenContainer">
        <div className="loginScreenTitleWrapper">
          <img src={zigitLogo} alt="Zigit" className="img" />
        </div>
        <div className="loginScreenFormWrapper">
          <Typography variant="h4" component="div" className="title">
            Login
          </Typography>
          <form
            onSubmit={handleSubmit(onSubmit)}
            className="loginScreenForm"
            style={{}}
          >
            <ControlledTextField
              control={control}
              type="text"
              name="email"
              errors={errors}
              rules={{
                required: "Email is required",
                pattern: {
                  value:
                    /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/,
                  message: "Email is not valid",
                },
              }}
              label="Email"
            />
            <ControlledTextField
              control={control}
              type="password"
              name="password"
              label="Password"
              errors={errors}
              rules={{
                required: "Password is required",
                pattern: {
                  value: /^(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$/,
                  message: "Password is not valid",
                },
              }}
            />
            <FormHelperText error={Boolean(errorMessage)}>
              {errorMessage}
            </FormHelperText>

            <LoadingButton
              loading={isLoading}
              loadingIndicator="Loading..."
              variant="outlined"
              type="submit"
              className="btn"
            >
              Login
            </LoadingButton>
          </form>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
