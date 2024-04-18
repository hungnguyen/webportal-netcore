import { createAsyncThunk } from "@reduxjs/toolkit";
import * as service from "./productInCategoryService";
import { handleError } from "../common";

const actionRootType = "productInCategory";

export const getByProductIdAsync = createAsyncThunk(
  `${actionRootType}/getByProductId`,
  async (id, thunkApi) => {
    try {
      const res = await service.getByProductId(id);
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
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);

export const removeByProductIdAsync = createAsyncThunk(
  `${actionRootType}/removeByProductId`,
  async (id, thunkApi) => {
    try {
      await service.removeByProductId(id);
      return id;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);

export const removeByCategoryIdAsync = createAsyncThunk(
  `${actionRootType}/removeByCategoryId`,
  async (id, thunkApi) => {
    try {
      await service.removeByCategoryId(id);
      return id;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);
