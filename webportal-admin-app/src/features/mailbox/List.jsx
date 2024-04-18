import React, { useState, useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { mailboxSelector } from "../../redux/mailbox/mailboxSlice";
import {
  getPagingAsync,
  removeAsync,
} from "../../redux/mailbox/mailboxAsyncThunk";
import useStyles from "../shared/styles";
import { NavLink } from "react-router-dom";
import ConfirmDialog from "../shared/ConfirmDialog";
import TableView from "../shared/TableView";
import { Paper, IconButton, Tooltip } from "@material-ui/core";
import { Visibility, Delete } from "@material-ui/icons";
import Moment from "react-moment";
import { applicationSelector } from "../../redux/application/applicationSlice";
import { useTranslation } from "react-i18next";

export default function List() {
  const dispatch = useDispatch();
  const mailbox = useSelector(mailboxSelector);
  const classes = useStyles();
  const [openConfirmDelete, setOpenConfirmDelete] = useState(false);
  const [itemDelete, setItemDelete] = useState({});
  const application = useSelector(applicationSelector);
  const { t } = useTranslation();

  const loadData = () => {
    dispatch(
      getPagingAsync({
        pagesize: 0,
        websiteid: application.website.id,
        languageid: application.languageid,
      })
    );
  };

  useEffect(() => {
    if (mailbox.list.length === 0) {
      loadData();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleDelete = (item) => {
    setOpenConfirmDelete(true);
    setItemDelete(item);
  };

  const handleConfirm = () => {
    dispatch(removeAsync(itemDelete.id));
    setOpenConfirmDelete(false);
  };

  const handleCose = () => {
    setOpenConfirmDelete(false);
  };

  const columns = [
    { field: "fromemail", headerName: t("from-email"), flex: 0.5 },
    {
      field: "subject",
      headerName: t("subject"),
      flex: 1,
      renderCell: (params) => (
        <NavLink to={`/mailbox/detail/${params.id}`} className={classes.link}>
          {params.row.subject}
        </NavLink>
      ),
    },
    { field: "orderid", headerName: t("order-id"), flex: 0.5 },
    {
      field: "datecreated",
      headerName: t("date-created"),
      flex: 0.5,
      type: "date",
      renderCell: (params) => (
        <Moment format="DD/MM/YYYY">{params.value}</Moment>
      ),
    },
    {
      field: "id",
      headerName: t("action"),
      flex: 0.5,
      renderCell: (params) => (
        <strong>
          <NavLink to={`/mailbox/detail/${params.id}`}>
            <Tooltip title={t("view")}>
              <IconButton color="primary" size="small">
                <Visibility />
              </IconButton>
            </Tooltip>
          </NavLink>
          <Tooltip title={t("delete")}>
            <IconButton
              color="secondary"
              size="small"
              onClick={() => handleDelete(params.row)}
            >
              <Delete />
            </IconButton>
          </Tooltip>
        </strong>
      ),
    },
  ];

  return (
    <>
      <Paper className={classes.tablePaper}>
        <TableView
          title={t("list")}
          rows={mailbox.list}
          loading={mailbox.loading}
          columns={columns}
          searchColumn={["subject"]}
          onRefresh={loadData}
        />
      </Paper>
      <ConfirmDialog
        title={t("confirm-delete")}
        message={t("are-you-sure-want-to-delete", {
          itemName: itemDelete.key,
        })}
        open={openConfirmDelete}
        handleClose={handleCose}
        handleConfirm={handleConfirm}
      />
    </>
  );
}
