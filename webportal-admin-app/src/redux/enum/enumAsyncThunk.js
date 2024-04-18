import { createAsyncThunk } from "@reduxjs/toolkit";
import * as service from "./enumService";
import { handleError } from "../common";

export const getStatusAsync = createAsyncThunk(
  "enums/getStatus",
  async (_, thunkApi) => {
    try {
      const res = await service.getStatus();
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);

export const getPositionAsync = createAsyncThunk(
  "enums/getPosition",
  async (_, thunkApi) => {
    try {
      const res = await service.getPosition();
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);

export const getGenderAsync = createAsyncThunk(
  "enums/getGender",
  async (_, thunkApi) => {
    try {
      const res = await service.getGender();
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);

export const getPayMethodAsync = createAsyncThunk(
  "enums/getPayMethod",
  async (_, thunkApi) => {
    try {
      const res = await service.getPayMethod();
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);

export const getPayStatusAsync = createAsyncThunk(
  "enums/getPayStatus",
  async (_, thunkApi) => {
    try {
      const res = await service.getPayStatus();
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);

export const getOrderStatusAsync = createAsyncThunk(
  "enums/getOrderStatus",
  async (_, thunkApi) => {
    try {
      const res = await service.getOrderStatus();
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);
