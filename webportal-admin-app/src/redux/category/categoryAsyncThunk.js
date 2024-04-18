import { createAsyncThunk } from "@reduxjs/toolkit";
import * as service from "./categoryService";
import * as uploadService from "../upload/uploadService";
import { handleError, handleSuccess, Operation } from "../common";

const actionRootType = "category";

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

export const getByIdAsync = createAsyncThunk(
  `${actionRootType}/getById`,
  async (id, thunkApi) => {
    try {
      const res = await service.getById(id);
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);

export const getPagingAsync = createAsyncThunk(
  `${actionRootType}/getPaging`,
  async (data, thunkApi) => {
    try {
      const res = await service.getPaging(data);
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
      //upload image
      let image = data.item.image;
      if (data.imageData) {
        const imageRes = await uploadService.uploadImage(data.imageData);
        image = imageRes.data.filename;
      }
      //upload icon
      let icon = data.item.icon;
      if (data.iconData) {
        const iconRes = await uploadService.uploadImage(data.iconData);
        icon = iconRes.data.filename;
      }
      //create category
      const res = await service.create({
        ...data.item,
        image,
        icon,
      });

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
      //upload image
      let image = data.item.image;
      if (data.imageData) {
        const imageRes = await uploadService.uploadImage(data.imageData);
        image = imageRes.data.filename;
      }
      //upload icon
      let icon = data.item.icon;
      if (data.iconData) {
        const iconRes = await uploadService.uploadImage(data.iconData);
        icon = iconRes.data.filename;
      }
      //update category
      const res = await service.update({
        ...data.item,
        image,
        icon,
      });

      handleSuccess(thunkApi.dispatch, Operation.Update, actionRootType);
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);

export const removeAsync = createAsyncThunk(
  `${actionRootType}/remove`,
  async (id, thunkApi) => {
    try {
      await service.remove(id);

      handleSuccess(thunkApi.dispatch, Operation.Delete, actionRootType);
      return id;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);
