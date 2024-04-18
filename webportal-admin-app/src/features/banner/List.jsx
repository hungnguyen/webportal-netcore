import React, { useState, useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { bannerSelector } from "../../redux/banner/bannerSlice";
import {
  getPagingAsync,
  removeAsync,
} from "../../redux/banner/bannerAsyncThunk";
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
  const banner = useSelector(bannerSelector);
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
    if (banner.list.length === 0) {
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
      field: "name",
      headerName: t("name"),
      flex: 1,
      renderCell: (params) => (
        <NavLink to={`/banner/edit/${params.id}`} className={classes.link}>
          {params.row.name}
        </NavLink>
      ),
    },
    {
      field: "image",
      headerName: t("image"),
      flex: 1,
      renderCell: (params) => (
        <img
          src={`${application.imageBaseAddress}/${params.row.image}`}
          alt=""
          height="100%"
        />
      ),
    },
    {
      field: "position",
      headerName: t("position"),
      flex: 1,
      renderCell: (params) => (
        <>{t(params.row.position.toLowerCase())}</>
      ),
    },
    {
      field: "status",
      headerName: t("status"),
      flex: 1,
      renderCell: (params) => (
        <>{t(params.row.status)}</>
      ),
    },
    {
      field: "dateupdated",
      headerName: t("date-updated"),
      flex: 0.5,
      type: "date",
      renderCell: (params) => (
        <Moment format="DD/MM/YYYY">{params.value}</Moment>
      ),
    },
    { field: "updatedby", headerName: t("update-by"), flex: 0.5 },
    {
      field: "id",
      headerName: t("action"),
      flex: 0.5,
      renderCell: (params) => (
        <strong>
          <NavLink to={`/banner/edit/${params.id}`}>
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
        <NavLink to="/banner/edit" className={classes.link}>
          {t("add-new")}
        </NavLink>
      </Button>
      <Paper className={classes.tablePaper}>
        <TableView
          title={t("list")}
          rows={banner.list}
          loading={banner.loading}
          columns={columns}
          searchColumn={["name"]}
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
