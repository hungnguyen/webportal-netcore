import { createAsyncThunk } from "@reduxjs/toolkit";
import * as service from "./appuserroleService";
import { handleError } from "../common";

const actionRootType = "appuserrole";

export const getByUserIdAsync = createAsyncThunk(
  `${actionRootType}/getByUserId`,
  async (id, thunkApi) => {
    try {
      const res = await service.getByUserId(id);
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);

export const roleAssignAsync = createAsyncThunk(
  `${actionRootType}/roleAssign`,
  async (data, thunkApi) => {
    try {
      const res = await service.roleAssign(data);
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
    }
  }
);
