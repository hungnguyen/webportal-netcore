import React, { useState } from "react";
import { DataGrid } from "@material-ui/data-grid";
import TableLoading from "./TableLoading";
import {
  Toolbar,
  Typography,
  InputBase,
  IconButton,
  Tooltip,
  Chip,
} from "@material-ui/core";
import { Search, Refresh } from "@material-ui/icons";
import useStyles from "./styles";
import { useTranslation } from "react-i18next";

export default function TableView({
  rows,
  columns,
  loading,
  searchColumn,
  onRefresh,
  title = "List",
  hideFooter = false,
  height = 650,
  rowHeight = 52,
}) {
  const classes = useStyles();
  const [keyword, setKeyword] = useState("");
  const { t } = useTranslation();

  const handleSearch = (e) => {
    setKeyword(e.target.value);
  };

  return (
    <>
      <Toolbar className={classes.toolbar}>
        <Typography variant="h6" className={classes.title}>
          {title}{" "}
          {
            <Chip
              label={rows.length}
              color="primary"
              variant="outlined"
              size="small"
            />
          }
        </Typography>
        <Tooltip title={t("refresh-lish")}>
          <IconButton
            aria-label="delete"
            className={classes.margin}
            onClick={onRefresh}
          >
            <Refresh fontSize="inherit" />
          </IconButton>
        </Tooltip>
        <div className={classes.search}>
          <div className={classes.searchIcon}>
            <Search />
          </div>
          <InputBase
            placeholder={t("search")}
            classes={{
              root: classes.inputRoot,
              input: classes.inputInput,
            }}
            inputProps={{ "aria-label": "search" }}
            onChange={handleSearch}
          />
        </div>
      </Toolbar>
      <div style={{ height: height, width: "100%" }}>
        <DataGrid
          rows={rows.filter((x) => {
            let result = undefined;
            searchColumn.forEach((y) => {
              let arr = y.split(".");
              if (arr.length > 1) {
                result =
                  result ||
                  (x[arr[0]][arr[1]] &&
                    x[arr[0]][arr[1]]
                      .toString()
                      .toLowerCase()
                      .includes(keyword.toLowerCase()));
              } else {
                result =
                  result ||
                  (x[y] &&
                    x[y]
                      .toString()
                      .toLowerCase()
                      .includes(keyword.toLowerCase()));
              }
            });
            return result;
          })}
          columns={columns}
          pageSize={10}
          pagination
          hideFooter={hideFooter}
          rowsPerPageOptions={[5, 10, 20]}
          loading={loading}
          components={{
            LoadingOverlay: TableLoading,
          }}
          rowHeight={rowHeight}
        />
      </div>
    </>
  );
}
