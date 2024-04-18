import React, { useState, useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { orderSelector } from "../../redux/order/orderSlice";
import { getPagingAsync, removeAsync } from "../../redux/order/orderAsyncThunk";
import useStyles from "../shared/styles";
import { NavLink } from "react-router-dom";
import ConfirmDialog from "../shared/ConfirmDialog";
import TableView from "../shared/TableView";
import { Paper, IconButton, Tooltip } from "@material-ui/core";
import { Visibility, Delete } from "@material-ui/icons";
import Moment from "react-moment";
import NumberFormat from "react-number-format";
import { applicationSelector } from "../../redux/application/applicationSlice";
import { useTranslation } from "react-i18next";

export default function List() {
  const dispatch = useDispatch();
  const order = useSelector(orderSelector);
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
    if (order.list.length === 0) {
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
      field: "id",
      headerName: t("order-id"),
      flex: 0.5,
      renderCell: (params) => (
        <NavLink to={`/order/detail/${params.row.id}`} className={classes.link}>
          {params.row.id}
        </NavLink>
      ),
    },
    { field: "customername", headerName: t("customer"), flex: 1 },
    {
      field: "orderstatus",
      headerName: t("status"),
      flex: 0.5,
      renderCell: (params) => (
        <>{t(params.row.orderstatus)}</>
      ),
    },
    {
      field: "totalamout",
      headerName: t("total-amount"),
      flex: 0.5,
      renderCell: (params) => (
        <NumberFormat
          displayType="text"
          value={params.value}
          thousandSeparator
        />
      ),
    },
    {
      field: "orderdate",
      headerName: t("order-date"),
      flex: 0.5,
      type: "date",
      renderCell: (params) => (
        <Moment format="DD/MM/YYYY">{params.value}</Moment>
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
      field: "dummy",
      headerName: t("action"),
      flex: 0.5,
      renderCell: (params) => (
        <strong>
          <NavLink to={`/order/detail/${params.row.id}`}>
            <Tooltip title={t("edit")}>
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
          rows={order.list}
          loading={order.loading}
          columns={columns}
          searchColumn={["id"]}
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
