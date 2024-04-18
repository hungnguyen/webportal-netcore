import React, { useState, useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { customerSelector } from "../../redux/customer/customerSlice";
import {
  getPagingAsync,
  removeAsync,
} from "../../redux/customer/customerAsyncThunk";
import useStyles from "../shared/styles";
import { NavLink } from "react-router-dom";
import ConfirmDialog from "../shared/ConfirmDialog";
import TableView from "../shared/TableView";
import { Paper, IconButton, Button, Tooltip } from "@material-ui/core";
import { Edit, Delete } from "@material-ui/icons";

import Moment from "react-moment";
import { applicationSelector } from "../../redux/application/applicationSlice";
import { useTranslation } from "react-i18next";

export default function List() {
  const dispatch = useDispatch();
  const customer = useSelector(customerSelector);
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
    if (customer.list.length === 0) {
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
    {
      field: "fullname",
      headerName: t("full-name"),
      flex: 1,
      renderCell: (params) => (
        <NavLink to={`/customer/edit/${params.id}`} className={classes.link}>
          {params.row.fullname}
        </NavLink>
      ),
    },
    { field: "email", headerName: t("email"), flex: 1 },
    {
      field: "status",
      headerName: t("status"),
      flex: 1,
      renderCell: (params) => (
        <>{t(params.row.status)}</>
      ),
    },
    {
      field: "lastlogindate",
      headerName: t("last-login"),
      width: 250,
      type: "date",
      renderCell: (params) => (
        <Moment format="DD/MM/YYYY">{params.value}</Moment>
      ),
    },
    {
      field: "datecreated",
      headerName: t("date-created"),
      width: 250,
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
          <NavLink to={`/customer/edit/${params.id}`}>
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
        <NavLink to="/customer/edit" className={classes.link}>
          {t("add-new")}
        </NavLink>
      </Button>
      <Paper className={classes.tablePaper}>
        <TableView
          title={t("list")}
          rows={customer.list}
          loading={customer.loading}
          columns={columns}
          searchColumn={["fullname", "username", "email", "phonenumber"]}
          onRefresh={loadData}
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
