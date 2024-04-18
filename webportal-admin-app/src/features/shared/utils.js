import React from "react";

export function convertDateFromUTC(strDateTimeUtc) {
  let strDate = strDateTimeUtc.split("T")[0];
  let arr = strDate.split("-");
  return new Date(parseInt(arr[0]), parseInt(arr[1]) - 1, parseInt(arr[2]));
}

export function convertDateTimeFromUTC(strDateTimeUtc) {
  let strDate = strDateTimeUtc.split("T")[0];
  let strTime = strDateTimeUtc.split("T")[1].split(".")[0];
  let arr = strDate.split("-");
  let arrtime = strTime.split(":");
  return new Date(
    Date.UTC(
      parseInt(arr[0]),
      parseInt(arr[1]) - 1,
      parseInt(arr[2]),
      parseInt(arrtime[0]),
      parseInt(arrtime[1]),
      parseInt(arrtime[2])
    )
  );
}

export function getDateOnly(strDateTimeUtc) {
  if (strDateTimeUtc) return strDateTimeUtc.split("T")[0];
  else return strDateTimeUtc;
}

export const renderHTML = (rawHTML) =>
  React.createElement("div", { dangerouslySetInnerHTML: { __html: rawHTML } });

export const getEnumKey = (enums, value) => {
  let item = enums.find((i) => i.value === value);
  return item?.key;
};

export const queryString = (obj) => {
  var str = [];
  for (var p in obj)
    if (obj.hasOwnProperty(p)) {
      str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
    }
  return str.join("&");
};

export function compareDate(date1, date2) {
  let result = 0;
  if (date1.getTime() > date2.getTime()) {
    result = 1;
  } else if (date1.getTime() < date2.getTime()) {
    result = -1;
  }
  return result;
}

//handler ajax error
export function handlerError(error) {
  let statusCode = 0;
  if (error?.status) {
    statusCode = parseInt(error?.status);
  }
  let res = { statusCode, failed: true, data: error?.data };
  switch (statusCode) {
    case 400:
      return { ...res, statusText: "Bad Request" };
    case 401:
      return { ...res, statusText: "Unauthorized" };
    case 404:
      return { ...res, statusText: "Not Found" };
    case 405:
      return { ...res, statusText: "Method Not Allowed" };
    case 406:
      return { ...res, statusText: "Not Acceptable" };
    case 413:
      return {
        ...res,
        statusText: "Request Entity Too Large",
      };
    case 415:
      return {
        ...res,
        statusText: "Unsupported Media Type",
      };
    default:
      return { ...res, statusText: "Internal Server Error" };
  }
}

export function getFileExtension(fileName){
  return fileName.split('.').pop();
}

export function equals(value1, value2){
  if(typeof value1 === "number"){
    return parseFloat(value1) === parseFloat(value2);
  }
  else {
    return value1 === value2;
  }
}