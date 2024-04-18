import axios from "axios";
import { queryString } from "../../features/shared/utils";
const apiUrl = "/mailboxes";

export const getAll = () => {
  return axios.get(apiUrl);
};

export const getById = (id) => {
  return axios.get(`${apiUrl}/${id}`);
};

export const getPaging = (data) => {
  return axios.get(`${apiUrl}/getPaging?${queryString(data)}`);
};

export const create = (data) => {
  return axios.post(apiUrl, data);
};

export const update = (data) => {
  return axios.put(`${apiUrl}/${data.id}`, data);
};

export const remove = (id) => {
  return axios.delete(`${apiUrl}/${id}`);
};
