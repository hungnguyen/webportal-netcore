import React, { useState, useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { appuserSelector } from "../../redux/appuser/appuserSlice";
import {
  getAllAsync,
  removeAsync,
} from "../../redux/appuser/appuserAsyncThunk";
import useStyles from "../shared/styles";
import { NavLink } from "react-router-dom";
import ConfirmDialog from "../shared/ConfirmDialog";
import TableView from "../shared/TableView";
import {
  Paper,
  IconButton,
  Button,
  Tooltip,
  Typography,
} from "@material-ui/core";
import { Edit, Delete } from "@material-ui/icons";
import { convertDateTimeFromUTC, compareDate } from "../shared/utils";
import Moment from "react-moment";
import { useTranslation } from "react-i18next";

export default function List() {
  const dispatch = useDispatch();
  const appuser = useSelector(appuserSelector);
  const classes = useStyles();
  const [openConfirmDelete, setOpenConfirmDelete] = useState(false);
  const [itemDelete, setItemDelete] = useState({});
  const { t } = useTranslation();

  useEffect(() => {
    if (appuser.list.length === 0) {
      dispatch(getAllAsync());
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
    {
      field: "username",
      headerName: t("user-name"),
      flex: 1,
      renderCell: (params) => (
        <NavLink to={`/appuser/edit/${params.id}`} className={classes.link}>
          {params.row.username}
        </NavLink>
      ),
    },
    {
      field: "email",
      headerName: t("email"),
      flex: 1,
    },
    { field: "fullname", headerName: t("full-name"), flex: 1 },
    {
      field: "isonline",
      headerName: t("is-online"),
      flex: 0.5,
      renderCell: (params) => (
        <>
          {params.row.isonline ? (
            <Typography color="primary" variant="body2">
              {t("online")}
            </Typography>
          ) : (
            <Typography color="error" variant="body2">
              {t("offline")}
            </Typography>
          )}
        </>
      ),
    },
    {
      field: "lockoutend",
      headerName: t("status"),
      flex: 0.5,
      renderCell: (params) => (
        <>
          <Typography color="error" variant="body2">
            {params.row.lockoutend &&
            compareDate(
              convertDateTimeFromUTC(params.row.lockoutend),
              new Date()
            ) > 0
              ? t("locked")
              : ""}
          </Typography>
        </>
      ),
    },
    {
      field: "lastlogindate",
      headerName: t("last-login"),
      flex: 1,
      type: "date",
      renderCell: (params) => (
        <Moment format="DD/MM/YYYY">{params.value}</Moment>
      ),
    },
    { field: "accessfailedcount", headerName: t("access-failed"), flex: 0.5 },
    {
      field: "id",
      headerName: t("action"),
      flex: 0.5,
      renderCell: (params) => (
        <strong>
          <NavLink to={`/appuser/edit/${params.id}`}>
            <Tooltip title={t("edit")}>
              <IconButton color="primary" size="small">
                <Edit />
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
      <Button variant="outlined" color="primary">
        <NavLink to="/appuser/edit" className={classes.link}>
          {t("add-new")}
        </NavLink>
      </Button>
      <Paper className={classes.tablePaper}>
        <TableView
          title={t("list")}
          rows={appuser.list}
          loading={appuser.loading}
          columns={columns}
          searchColumn={["fullname", "username"]}
          onRefresh={() => dispatch(getAllAsync())}
        />
      </Paper>
      <ConfirmDialog
        title={t("confirm-delete")}
        message={t("are-you-sure-want-to-delete", {
          itemName: itemDelete.name,
        })}
        open={openConfirmDelete}
        handleClose={handleCose}
        handleConfirm={handleConfirm}
      />
    </>
  );
}
