import TextField from "@mui/material/TextField";
import { Controller } from "react-hook-form";

const ControlledTextField = ({ control, name, rules, errors, label, type }) => {
  return (
    <Controller
      render={({ field }) => (
        <TextField
          {...field}
          variant="outlined"
          type={type}
          label={rules.required ? label + "*" : label}
          error={Boolean(errors[name]?.message)}
          helperText={errors[name]?.message}
        />
      )}
      control={control}
      rules={rules}
      name={name}
    />
  );
};

export default ControlledTextField;
