import { createSlice } from "@reduxjs/toolkit";
import * as asyncThunk from "./analyticsAsyncThunk";

const initialState = {
  loading: false,
  summary: {},
  graph: {
    pageviews: {},
    activeusers: {},
    itempurchased: {},
  },
  toplist: {
    pageviews: {},
    items: {},
    cities: {},
    browsers: {},
    events: {},
  },
  error: "",
  isloaded: false,
};

export const analyticsSlice = createSlice({
  name: "analytics",
  initialState,
  reducers: {
    setLoaded: (state) => {
      state.isloaded = true;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(asyncThunk.getSummaryAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.getSummaryAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          if (action.payload.status === 400) {
            state.error = action.payload.error;
          } else {
            state.summary = action.payload;
          }
        }
      })

      .addCase(asyncThunk.getGraphAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.getGraphAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          if (action.payload.status === 400) {
            state.error = action.payload.error;
          } else {
            state.graph = {
              ...state.graph,
              [action.payload.type]: action.payload.data,
            };
          }
        }
      })

      .addCase(asyncThunk.getTopListAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(asyncThunk.getTopListAsync.fulfilled, (state, action) => {
        state.loading = false;
        if (action.payload) {
          if (action.payload.status === 400) {
            state.error = action.payload.error;
          } else {
            state.toplist = {
              ...state.toplist,
              [action.payload.type]: action.payload.data,
            };
          }
        }
      });
  },
});

export const { setLoaded } = analyticsSlice.actions;

export const analyticsSelector = (state) => state.analytics;

export default analyticsSlice.reducer;
