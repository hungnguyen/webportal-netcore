import axios from "axios";

const apiUrl = "/websites";

export const getAll = () => {
  return axios.get(apiUrl);
};

export const getById = (id) => {
  return axios.get(`${apiUrl}/${id}`);
};

export const getByDomain = (domain) => {
  return axios.get(`${apiUrl}/getByDomain?domain=${domain}`);
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
