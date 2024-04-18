import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import { store } from "./app/store";
import { Provider } from "react-redux";
import * as serviceWorker from "./serviceWorker";
import axios from "axios";
import { HashRouter as Router } from "react-router-dom";
import { SnackbarProvider } from "notistack";
import "./i18n/config";

axios.defaults.baseURL = process.env.REACT_APP_BASE_API_URL ?? "/api";
axios.defaults.headers.post["Content-Type"] = "application/json";
axios.defaults.withCredentials = true;

ReactDOM.render(
  <Provider store={store}>
    <Router>
      <SnackbarProvider maxSnack={3}>
        <App />
      </SnackbarProvider>
    </Router>
  </Provider>,
  document.getElementById("root")
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
