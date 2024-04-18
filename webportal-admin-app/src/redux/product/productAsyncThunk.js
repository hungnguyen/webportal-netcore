import { createAsyncThunk } from "@reduxjs/toolkit";
import * as service from "./productService";
import * as uploadService from "../upload/uploadService";
import * as productInCategoryService from "../productInCategory/productInCategoryService";
import { handleError, handleSuccess, Operation } from "../common";

const actionRootType = "product";

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
      let filename = data.item.filename;
      if (data.imageData) {
        const res = await uploadService.uploadImage(data.imageData);
        filename = res.data.filename;
      }

      //create product
      const productRes = await service.create({
        ...data.item,
        image: filename,
      });

      //create product in category
      await productInCategoryService.create({
        productid: productRes.data.id,
        catids: data.inCategories,
      });

      handleSuccess(thunkApi.dispatch, Operation.Add, actionRootType);
      return productRes.data;
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
      let filename = data.item.filename;
      if (data.imageData) {
        const res = await uploadService.uploadImage(data.imageData);
        filename = res.data.filename;
      }
      //remove old records product in category
      await productInCategoryService.removeByProductId(data.item.id);
      //create new product in category
      await productInCategoryService.create({
        productid: data.item.id,
        catids: data.inCategories,
      });
      //update product
      const productRes = await service.update({
        ...data.item,
        image: filename,
      });

      handleSuccess(thunkApi.dispatch, Operation.Update, actionRootType);
      return productRes.data;
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

export const getCountAsync = createAsyncThunk(
  `${actionRootType}/getCount`,
  async (data, thunkApi) => {
    try {
      const res = await service.getCount(data);
      return { typecode: data.typecode, total: res.data };
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);
