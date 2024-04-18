import axios from "axios";
const apiUrl = "/enums";

export const getStatus = () => {
  return axios.get(`${apiUrl}/getstatus`);
};

export const getGender = () => {
  return axios.get(`${apiUrl}/getgender`);
};

export const getPosition = () => {
  return axios.get(`${apiUrl}/getposition`);
};

export const getPayMethod = () => {
  return axios.get(`${apiUrl}/getpaymethod`);
};

export const getPayStatus = () => {
  return axios.get(`${apiUrl}/getpaystatus`);
};

export const getOrderStatus = () => {
  return axios.get(`${apiUrl}/getorderstatus`);
};
