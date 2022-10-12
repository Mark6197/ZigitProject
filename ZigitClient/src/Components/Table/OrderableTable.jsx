import React, { useState, useEffect } from "react";
import {
  Box,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TablePagination,
  TableRow,
  TableSortLabel,
  Paper,
  IconButton,
  TextField,
} from "@mui/material";
import { visuallyHidden } from "@mui/utils";
import FilterListIcon from "@mui/icons-material/FilterList";
import useToggle from "../../Hooks/useToggle";

const descendingComparator = (a, b, orderBy) => {
  if (b[orderBy] < a[orderBy]) {
    return -1;
  }
  if (b[orderBy] > a[orderBy]) {
    return 1;
  }
  return 0;
};

const getComparator = (order, orderBy) => {
  return order === "desc"
    ? (a, b) => descendingComparator(a, b, orderBy)
    : (a, b) => -descendingComparator(a, b, orderBy);
};

const OrderableTableHead = ({ order, orderBy, onRequestSort, headCells }) => {
  const createSortHandler = (property) => (event) => {
    onRequestSort(event, property);
  };

  return (
    <TableHead>
      <TableRow>
        {headCells.map((headCell) => (
          <TableCell
            key={headCell.id}
            sortDirection={orderBy === headCell.id ? order : false}
          >
            <TableSortLabel
              active={orderBy === headCell.id}
              direction={orderBy === headCell.id ? order : "asc"}
              onClick={createSortHandler(headCell.id)}
            >
              {headCell.label}
              {orderBy === headCell.id ? (
                <Box component="span" sx={visuallyHidden}>
                  {order === "desc" ? "sorted descending" : "sorted ascending"}
                </Box>
              ) : null}
            </TableSortLabel>
          </TableCell>
        ))}
      </TableRow>
    </TableHead>
  );
};

const OrderableTable = ({
  rows,
  headCells,
  defaultQueryParameters,
  filterRows,
}) => {
  const [order, setOrder] = useState("asc");
  const [orderBy, setOrderBy] = useState("name");
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [isFilterOpen, setIsFilterOpen] = useToggle();
  const [queryParameters, setQueryParameters] = useState(
    defaultQueryParameters
  );
  const [filteredRows, setFilteredRows] = useState(rows);

  useEffect(() => {
    const fRows = filterRows(queryParameters);
    setFilteredRows(fRows);
  }, [queryParameters, rows]);

  const handleRequestSort = (event, property) => {
    const isAsc = orderBy === property && order === "asc";
    setOrder(isAsc ? "desc" : "asc");
    setOrderBy(property);
  };

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  // Avoid a layout jump when reaching the last page with empty rows.
  const emptyRows =
    page > 0 ? Math.max(0, (1 + page) * rowsPerPage - filteredRows.length) : 0;

  const onFilterChange = (e) => {
    console.log(e.target.id, e.target.value);
    setQueryParameters((prev) => {
      return { ...prev, [e.target.id]: e.target.value };
    });
  };
  return (
    <Paper
      sx={{
        marginTop: "10px",
        width: "90%",
        padding: "20px",
        paddingTop: "10px",
      }}
    >
      <IconButton onClick={setIsFilterOpen}>
        <FilterListIcon />
      </IconButton>{" "}
      Filter
      {isFilterOpen && (
        <div>
          <TextField
            id="id"
            label="Id"
            variant="outlined"
            onChange={onFilterChange}
            value={queryParameters.id}
          />
          <TextField
            id="name"
            label="Name"
            variant="outlined"
            onChange={onFilterChange}
            sx={{ marginLeft: "10px" }}
            value={queryParameters.name}
          />
        </div>
      )}
      <TableContainer>
        <Table sx={{ minWidth: 750 }} aria-labelledby="tableTitle">
          <OrderableTableHead
            order={order}
            orderBy={orderBy}
            onRequestSort={handleRequestSort}
            headCells={headCells}
          />
          <TableBody>
            {filteredRows
              .slice()
              .sort(getComparator(order, orderBy))
              .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
              .map((row) => {
                return (
                  <TableRow
                    tabIndex={-1}
                    key={row.id}
                    sx={{
                      backgroundColor:
                        row.score < 70
                          ? "#FF7F7F"
                          : row.score > 90
                          ? "#A1F7A7"
                          : "inherit",
                    }}
                  >
                    {headCells.map((hc, index) => {
                      return (
                        <TableCell key={row.id + hc.id}>{row[hc.id]}</TableCell>
                      );
                    })}
                  </TableRow>
                );
              })}
            {emptyRows > 0 && (
              <TableRow
                style={{
                  height: 33 * emptyRows,
                }}
              >
                <TableCell colSpan={6} />
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>
      <TablePagination
        rowsPerPageOptions={[5, 10, 25, 100]}
        component="div"
        count={filteredRows.length}
        rowsPerPage={rowsPerPage}
        page={page}
        onPageChange={handleChangePage}
        onRowsPerPageChange={handleChangeRowsPerPage}
      />
    </Paper>
  );
};

export default OrderableTable;
