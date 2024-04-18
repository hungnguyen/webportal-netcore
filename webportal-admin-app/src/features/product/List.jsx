import React, { useState, useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { productSelector } from "../../redux/product/productSlice";
import {
  getPagingAsync,
  removeAsync,
} from "../../redux/product/productAsyncThunk";
import useStyles from "../shared/styles";
import { NavLink, useParams } from "react-router-dom";
import ConfirmDialog from "../shared/ConfirmDialog";
import TableView from "../shared/TableView";
import { Paper, IconButton, Button, Tooltip } from "@material-ui/core";
import { Edit, Delete, Image } from "@material-ui/icons";

import Moment from "react-moment";
import { applicationSelector } from "../../redux/application/applicationSlice";
import { useTranslation } from "react-i18next";

export default function List() {
  const { type } = useParams();
  const dispatch = useDispatch();
  const product = useSelector(productSelector);
  const classes = useStyles();
  const [openConfirmDelete, setOpenConfirmDelete] = useState(false);
  const [itemDelete, setItemDelete] = useState({});
  const [productByType, setProtuctByType] = useState([]);
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
    if (product.list.length === 0) {
      loadData();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    setProtuctByType(product.list.filter((i) => i.typecode === type));
  }, [type, product.list]);

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
    { field: "id", headerName: t("id"), flex: 0.3 },
    {
      field: "name",
      headerName: t("name"),
      flex: 1,
      renderCell: (params) => (
        <NavLink
          to={`/product/${type}/edit/${params.id}`}
          className={classes.link}
        >
          {params.row.name}
        </NavLink>
      ),
    },

    {
      field: "status",
      headerName: t("status"),
      flex: 0.5,
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
    { field: "viewcount", headerName: t("view-count"), flex: 0.5 },
    { field: "updatedby", headerName: t("update-by"), flex: 0.5 },
    {
      field: "dummy",
      headerName: t("action"),
      flex: 0.5,
      renderCell: (params) => (
        <strong>
          <NavLink to={`/product/${type}/edit/${params.id}`}>
            <Tooltip title={t("edit")}>
              <IconButton color="primary" size="small">
                <Edit />
              </IconButton>
            </Tooltip>
          </NavLink>
          <NavLink to={`/product-file/${type}/${params.id}`}>
            <Tooltip title={t("image")}>
              <IconButton className={classes.successButton} size="small">
                <Image />
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
        <NavLink to={`/product/${type}/edit`} className={classes.link}>
          {t("add-new")}
        </NavLink>
      </Button>
      <Paper className={classes.tablePaper}>
        <TableView
          title={t("list")}
          rows={productByType}
          loading={product.loading}
          columns={columns}
          searchColumn={["name", "id"]}
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
