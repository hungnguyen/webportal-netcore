import React, { useState } from "react";
import { Alert } from "@material-ui/lab";
import { LockOutlined } from "@material-ui/icons";
import { makeStyles } from "@material-ui/core/styles";
import {
  CssBaseline,
  TextField,
  FormControlLabel,
  Checkbox,
  Link,
  Grid,
  Box,
  Typography,
  Container,
  Avatar,
  Button,
} from "@material-ui/core";
import { loginAsync } from "../redux/account/accountAsyncThunk";
import { accountSelector } from "../redux/account/accountSlice";
import { useSelector, useDispatch } from "react-redux";

const useStyles = makeStyles((theme) => ({
  paper: {
    marginTop: theme.spacing(8),
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.secondary.main,
  },
  form: {
    width: "100%", // Fix IE 11 issue.
    marginTop: theme.spacing(1),
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
}));

function Login() {
  const classes = useStyles();
  const dispatch = useDispatch();
  const account = useSelector(accountSelector);
  const [loginmodel, setLoginModel] = useState({
    username: "",
    password: "",
    rememberme: true,
  });

  const doLogin = (e) => {
    e.preventDefault();
    dispatch(loginAsync(loginmodel));
  };
  const handleChange = (e) => {
    const { name, value, checked, type } = e.target;
    setLoginModel({
      ...loginmodel,
      [name]: type === "checkbox" ? checked : value,
    });
  };
  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Avatar className={classes.avatar}>
          <LockOutlined />
        </Avatar>
        <Typography component="h1" variant="h5">
          Web Portal 4.0 - Sign In
        </Typography>
        {account.error !== "" && (
          <Alert severity="error">{account.error}</Alert>
        )}
        <form className={classes.form} noValidate>
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            id="username"
            label="Username"
            name="username"
            autoComplete="username"
            autoFocus
            value={loginmodel.username}
            onChange={handleChange}
          />
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            name="password"
            label="Password"
            type="password"
            id="password"
            onChange={handleChange}
          />
          <FormControlLabel
            control={
              <Checkbox
                value="remember"
                color="primary"
                checked={loginmodel.rememberme}
                onChange={handleChange}
              />
            }
            label="Remember me"
          />
          <Button
            fullWidth
            variant="contained"
            color="primary"
            className={classes.submit}
            onClick={(e) => doLogin(e)}
            disabled={account.loading}
            type="submit"
          >
            {account.loading ? "Signing In..." : "Sign In"}
          </Button>
          <Grid container>
            <Grid item xs>
              <Link href="#" variant="body2">
                Forgot password?
              </Link>
            </Grid>
          </Grid>
        </form>
      </div>
      <Box mt={8}>
        <Typography variant="body2" color="textSecondary" align="center">
          {"Copyright © "}
          {new Date().getFullYear()}
        </Typography>
      </Box>
    </Container>
  );
}

export default Login;
