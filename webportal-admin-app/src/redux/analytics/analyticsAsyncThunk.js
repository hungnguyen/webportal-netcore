import { createAsyncThunk } from "@reduxjs/toolkit";
import * as service from "./analyticsService";
import { handleError } from "../common";

export const getSummaryAsync = createAsyncThunk(
  "analytics/summary",
  async (data, thunkApi) => {
    try {
      const res = await service.getSummary(data);
      return res.data;
    } catch (e) {
      handleError(thunkApi.dispatch, e);
      return e.response.data;
    }
  }
);

export const getGraphAsync = createAsyncThunk(
  "analytics/graph",
  async (data, thunkApi) => {
    try {
      const res = await service.getGraph(data.request);
      return {
        type: data.type,
        data: res.data,
      };
    } catch (e) {
      handleError(thunkApi.dispatch, e);
      return e.response.data;
    }
  }
);

export const getTopListAsync = createAsyncThunk(
  "analytics/toplist",
  async (data, thunkApi) => {
    try {
      const res = await service.getTopList(data.request);
      return {
        type: data.type,
        data: res.data,
      };
    } catch (e) {
      handleError(thunkApi.dispatch, e);
      return e.response.data;
    }
  }
);
