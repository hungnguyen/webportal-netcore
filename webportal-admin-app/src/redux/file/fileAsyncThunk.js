import { createAsyncThunk } from "@reduxjs/toolkit";
import * as service from "./fileService";
import { handleError, handleSuccess, Operation } from "../common";

const actionRootType = "file";

export const getAllAsync = createAsyncThunk(
  `${actionRootType}/getAll`,
  async (_, thunkApi) => {
    try {
      const res = await service.getAll();
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);

export const getSubAsync = createAsyncThunk(
  `${actionRootType}/getSub`,
  async (name, thunkApi) => {
    try {
      const res = await service.getSub(name);
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);

export const createAsync = createAsyncThunk(
  `${actionRootType}/create`,
  async (data, thunkApi) => {
    try {
      const res = await service.create(data);

      handleSuccess(thunkApi.dispatch, Operation.Add, actionRootType);
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);

export const updateAsync = createAsyncThunk(
  `${actionRootType}/update`,
  async (data, thunkApi) => {
    try {
      await service.update(data);

      handleSuccess(thunkApi.dispatch, Operation.Update, actionRootType);
      return data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);

export const removeAsync = createAsyncThunk(
  `${actionRootType}/remove`,
  async (name, thunkApi) => {
    try {
      await service.remove(name);

      handleSuccess(thunkApi.dispatch, Operation.Delete, actionRootType);
      return name;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);
