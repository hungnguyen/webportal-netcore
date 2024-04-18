import { logout } from "./account/accountSlice";
import { showNotice } from "./notice/noticeSlice";
import { addLog } from "./loginfo/logSlice";

export const Operation = {
	Add: "Add",
	Update: "Update",
	Delete: "Delete"
}

export function handleError(dispatch, error) {
  let statusCode = 0;
  if (error.response?.status) {
    statusCode = parseInt(error.response?.status);
  }
  switch (statusCode) {
    case 401:
      dispatch(logout());
      break;
    default:
      if (error.response) {
        // The request was made and the server responded with a status code
        // that falls out of the range of 2xx
        console.log(error.response);
        dispatch(
          showNotice({ type: "error", message: error.response.data.message ?? error.response.data.error })
        );
        dispatch(addLog(error.response));
      } else if (error.request) {
        // The request was made but no response was received
        // `error.request` is an instance of XMLHttpRequest in the browser and an instance of
        // http.ClientRequest in node.js
        console.log(error.request);
      } else {
        // Something happened in setting up the request that triggered an Error
        console.log("Error", error.message);
        dispatch(showNotice({ type: "error", message: error.message }));
      }

      break;
  }
}

export function handleSuccess(dispatch, operation, type){
  dispatch(showNotice({ type: "success", message: `${operation} ${type} successfully!` }))
}
