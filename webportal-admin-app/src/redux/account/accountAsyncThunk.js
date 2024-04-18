import { createAsyncThunk } from "@reduxjs/toolkit";
import * as service from "./accountService";
import { handleError } from "../common";

export const loginAsync = createAsyncThunk(
  "account/login",
  async (data, thunkApi) => {
    try {
      const res = await service.login(data);
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
      return e.response.data;
    }
  }
);

export const logoutAsync = createAsyncThunk(
  "account/logout",
  async (_, thunkApi) => {
    try {
      const res = await service.logout();
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);

export const getProfileAsync = createAsyncThunk(
  "account/getProfile",
  async (_, thunkApi) => {
    try {
      const res = await service.getProfile();
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);

export const checkLoginAsync = createAsyncThunk(
  "account/checkLogin",
  async (_, thunkApi) => {
    try {
      const res = await service.checkLogin();
      return res.status;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);
