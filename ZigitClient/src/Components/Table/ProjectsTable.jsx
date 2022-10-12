import React from "react";
import OrderableTable from "./OrderableTable";

const headCells = [
  {
    id: "id",
    numeric: false,
    label: "Id",
  },
  {
    id: "name",
    numeric: false,
    label: "Name",
  },
  {
    id: "score",
    numeric: true,
    label: "Score",
  },
  {
    id: "durationInDays",
    numeric: true,
    label: "Duration in days",
  },
  {
    id: "bugsCount",
    numeric: true,
    label: "Bugs count",
  },
];

const defaultQueryParameters = {
  id: "",
  name: "",
};

const ProjectsTable = ({ rows }) => {
  const filterRows = (queryParameters) =>
    rows.filter(
      (r) =>
        r.id.includes(queryParameters.id) &&
        r.name.toLowerCase().includes(queryParameters.name.toLowerCase())
    );
  return (
    <OrderableTable
      rows={rows}
      headCells={headCells}
      defaultQueryParameters={defaultQueryParameters}
      filterRows={filterRows}
    />
  );
};

export default ProjectsTable;
