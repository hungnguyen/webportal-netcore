import React, { useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { noticeSelector, hideNotice } from "../../redux/notice/noticeSlice";
import { useSnackbar } from "notistack";

function Notification() {
  const notice = useSelector(noticeSelector);
  const { enqueueSnackbar } = useSnackbar();
  const dispatch = useDispatch();

  useEffect(() => {
    notice.list.forEach((m) => {
      enqueueSnackbar(m.message, {
        variant: m.type, //default | error | success | warning | info
        onClose: () => {
          dispatch(hideNotice(m.id));
        }
      });
    });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [notice.list, enqueueSnackbar]);
  return <></>;
}

export default Notification;
