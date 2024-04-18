import axios from "axios";

const apiUrl = "/productincategories";

export const getByProductId = (id) => {
  return axios.get(`${apiUrl}/getByProductId?id=${id}`);
};

export const create = (data) => {
  return axios.post(apiUrl, data);
};

export const removeByProductId = (id) => {
  return axios.delete(`${apiUrl}/DeleteByProductId?id=${id}`);
};

export const removeByCategoryId = (id) => {
  return axios.delete(`${apiUrl}/DeleteByCategoryId?id=${id}`);
};
