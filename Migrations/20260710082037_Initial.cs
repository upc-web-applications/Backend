using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Acme.Center.Platform.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "access_logs",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    user_id = table.Column<string>(type: "longtext", nullable: true),
                    email = table.Column<string>(type: "longtext", nullable: false),
                    attempt_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    was_successful = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ip_address = table.Column<string>(type: "longtext", nullable: false),
                    failure_reason = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_access_logs", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "annual_ohs_plans",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    global_compliance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    goal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    completed_activities = table.Column<int>(type: "int", nullable: false),
                    total_activities = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_annual_ohs_plans", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "archived_reports",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    title = table.Column<string>(type: "longtext", nullable: false),
                    url = table.Column<string>(type: "longtext", nullable: false),
                    hash_integrity = table.Column<string>(type: "longtext", nullable: false),
                    archive_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_archived_reports", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "area_criticality_levels",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    sector_id = table.Column<string>(type: "longtext", nullable: true),
                    sector = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    criticality_level = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    map_intensity = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    last_updated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_area_criticality_levels", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "areas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    code = table.Column<string>(type: "varchar(255)", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    headquarters_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false),
                    risk_level = table.Column<string>(type: "longtext", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_areas", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "corrective_action_tickets",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ticket_number = table.Column<int>(type: "int", nullable: false),
                    report_id = table.Column<string>(type: "longtext", nullable: true),
                    sector_id = table.Column<string>(type: "longtext", nullable: true),
                    sector = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    risk_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    criticality_level = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    instructions = table.Column<string>(type: "longtext", nullable: false),
                    assigned_technician_id = table.Column<string>(type: "longtext", nullable: true),
                    technician_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    closure_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    sla_limit_hours = table.Column<int>(type: "int", nullable: false),
                    sla_missed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_corrective_action_tickets", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "critical_alerts",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    type = table.Column<string>(type: "longtext", nullable: false),
                    sector = table.Column<string>(type: "longtext", nullable: false),
                    risk_type = table.Column<string>(type: "longtext", nullable: false),
                    message = table.Column<string>(type: "longtext", nullable: false),
                    elapsed_hours = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false),
                    responsible_supervisor = table.Column<string>(type: "longtext", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_critical_alerts", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "critical_notifications",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ticket_id = table.Column<string>(type: "longtext", nullable: false),
                    supervisor_id = table.Column<string>(type: "longtext", nullable: true),
                    supervisor_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    message = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    sent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    sent_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_critical_notifications", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cumulative_st_indicators",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    total_incidents = table.Column<int>(type: "int", nullable: false),
                    resolved_incidents = table.Column<int>(type: "int", nullable: false),
                    compliance_rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    period = table.Column<string>(type: "longtext", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cumulative_st_indicators", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "daily_summaries",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    sector_id = table.Column<string>(type: "longtext", nullable: true),
                    sector = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    total_new = table.Column<int>(type: "int", nullable: false),
                    total_in_progress = table.Column<int>(type: "int", nullable: false),
                    total_resolved = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_daily_summaries", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dangers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    category = table.Column<string>(type: "longtext", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dangers", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dashboard_assets",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    code = table.Column<string>(type: "longtext", nullable: false),
                    sector_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dashboard_assets", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dashboard_technicians",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    specialty = table.Column<string>(type: "longtext", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dashboard_technicians", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dashboard_tickets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    sector_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "longtext", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false),
                    priority = table.Column<string>(type: "longtext", nullable: false),
                    assigned_technician_id = table.Column<string>(type: "longtext", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dashboard_tickets", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "generated_reports",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    type = table.Column<string>(type: "longtext", nullable: false),
                    month = table.Column<int>(type: "int", nullable: true),
                    year = table.Column<int>(type: "int", nullable: true),
                    format = table.Column<string>(type: "longtext", nullable: false),
                    generation_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    file_name = table.Column<string>(type: "longtext", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    sector_filter = table.Column<string>(type: "longtext", nullable: true),
                    size_kb = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_generated_reports", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "hazards",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    category = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    base_risk_level = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_hazards", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "headquarters",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    address = table.Column<string>(type: "longtext", nullable: false),
                    phone = table.Column<string>(type: "longtext", nullable: false),
                    email = table.Column<string>(type: "longtext", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_headquarters", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "heat_map_zones",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    sector_id = table.Column<int>(type: "int", nullable: false),
                    heat_index = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    risk_level = table.Column<string>(type: "longtext", nullable: false),
                    last_update = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_heat_map_zones", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "historical_incident_records",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    sector = table.Column<string>(type: "longtext", nullable: false),
                    incident_type = table.Column<string>(type: "longtext", nullable: false),
                    criticality = table.Column<string>(type: "longtext", nullable: false),
                    incident_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    resolved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    closing_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    resolution_time_hours = table.Column<int>(type: "int", nullable: true),
                    operator_id = table.Column<string>(type: "longtext", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_historical_incident_records", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "historical_trends",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    month = table.Column<int>(type: "int", nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    total_incidents = table.Column<int>(type: "int", nullable: false),
                    sector = table.Column<string>(type: "longtext", nullable: false),
                    type = table.Column<string>(type: "longtext", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_historical_trends", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inspections",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ticket = table.Column<string>(type: "varchar(255)", nullable: false),
                    incident_type = table.Column<string>(type: "longtext", nullable: false),
                    area_id = table.Column<int>(type: "int", nullable: false),
                    headquarters_id = table.Column<int>(type: "int", nullable: false),
                    asset_id = table.Column<int>(type: "int", nullable: true),
                    urgency_level = table.Column<string>(type: "longtext", nullable: false),
                    description = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false),
                    photo_url = table.Column<string>(type: "longtext", nullable: true),
                    operator_id = table.Column<string>(type: "longtext", nullable: false),
                    report_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    update_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    corrective_action = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inspections", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kpi_dashboards",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    goal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false),
                    update_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_kpi_dashboards", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "measure_verifications",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ticket_id = table.Column<string>(type: "longtext", nullable: false),
                    supervisor_id = table.Column<string>(type: "longtext", nullable: true),
                    supervisor_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    verdict = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    justification_comment = table.Column<string>(type: "longtext", nullable: false),
                    verification_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_measure_verifications", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mitigations",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    risk_assessment_id = table.Column<string>(type: "longtext", nullable: true),
                    ticket_id = table.Column<string>(type: "longtext", nullable: true),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    responsible = table.Column<string>(type: "longtext", nullable: false),
                    assigned_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    execution_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    result = table.Column<string>(type: "longtext", nullable: true),
                    observations = table.Column<string>(type: "longtext", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mitigations", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "monthly_reports",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    month = table.Column<int>(type: "int", nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    total_incidents = table.Column<int>(type: "int", nullable: false),
                    resolved_incidents = table.Column<int>(type: "int", nullable: false),
                    compliance_percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false),
                    generated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_monthly_reports", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "org_assets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    code = table.Column<string>(type: "varchar(255)", nullable: false),
                    serial_number = table.Column<string>(type: "longtext", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    area_id = table.Column<int>(type: "int", nullable: false),
                    headquarters_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false),
                    system_entry_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deactivation_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    acquisition_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    last_maintenance_date = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_org_assets", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pattern_alerts",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    pattern_id = table.Column<string>(type: "longtext", nullable: true),
                    sector_id = table.Column<string>(type: "longtext", nullable: true),
                    sector = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    risk_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    occurrence_count = table.Column<int>(type: "int", nullable: false),
                    first_report_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    generation_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pattern_alerts", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "photo_evidences",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    inspection_id = table.Column<int>(type: "int", nullable: false),
                    file_url = table.Column<string>(type: "longtext", nullable: false),
                    upload_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_photo_evidences", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "predictive_indicators",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    trend = table.Column<string>(type: "longtext", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_predictive_indicators", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "preventive_maintenances",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    asset_id = table.Column<string>(type: "longtext", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false),
                    scheduled_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_preventive_maintenances", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "risk_assessments",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    sector = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    hazard_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    probability = table.Column<int>(type: "int", nullable: false),
                    severity = table.Column<int>(type: "int", nullable: false),
                    risk_level = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    control_measures = table.Column<string>(type: "longtext", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    evaluation_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    user_id = table.Column<string>(type: "longtext", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_risk_assessments", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "risk_patterns",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    sector_id = table.Column<string>(type: "longtext", nullable: true),
                    sector = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    incident_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    hazard_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    frequency = table.Column<int>(type: "int", nullable: false),
                    first_occurrence_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    analysis_period_days = table.Column<int>(type: "int", nullable: false),
                    is_reviewed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    review_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    reviewed_by = table.Column<string>(type: "longtext", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_risk_patterns", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sessions",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    user_id = table.Column<string>(type: "longtext", nullable: false),
                    token_signature = table.Column<string>(type: "longtext", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    last_activity_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_valid = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    closed_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    close_reason = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sessions", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sla_alerts",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ticket_id = table.Column<string>(type: "longtext", nullable: false),
                    elapsed_hours = table.Column<int>(type: "int", nullable: false),
                    sla_limit_hours = table.Column<int>(type: "int", nullable: false),
                    alert_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    notified_to = table.Column<string>(type: "longtext", nullable: true),
                    notified_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sla_alerts", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "technicians",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    document_number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    full_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    specialty = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_technicians", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ticket_histories",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ticket_id = table.Column<string>(type: "longtext", nullable: false),
                    @event = table.Column<string>(name: "event", type: "varchar(100)", maxLength: 100, nullable: false),
                    user_id = table.Column<string>(type: "longtext", nullable: true),
                    user_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    details = table.Column<string>(type: "longtext", nullable: false),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket_histories", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(255)", nullable: false),
                    username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    email = table.Column<string>(type: "varchar(255)", nullable: false),
                    role = table.Column<string>(type: "longtext", nullable: false),
                    role_id = table.Column<string>(type: "longtext", nullable: false),
                    sector_id = table.Column<int>(type: "int", nullable: true),
                    account_status = table.Column<string>(type: "longtext", nullable: false),
                    password_hash = table.Column<string>(type: "longtext", nullable: false),
                    failed_attempts = table.Column<int>(type: "int", nullable: false),
                    locked_until = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_areas_code",
                table: "areas",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_headquarters_name",
                table: "headquarters",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_inspections_ticket",
                table: "inspections",
                column: "ticket",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_org_assets_code",
                table: "org_assets",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_roles_code",
                table: "roles",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_username",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "access_logs");

            migrationBuilder.DropTable(
                name: "annual_ohs_plans");

            migrationBuilder.DropTable(
                name: "archived_reports");

            migrationBuilder.DropTable(
                name: "area_criticality_levels");

            migrationBuilder.DropTable(
                name: "areas");

            migrationBuilder.DropTable(
                name: "corrective_action_tickets");

            migrationBuilder.DropTable(
                name: "critical_alerts");

            migrationBuilder.DropTable(
                name: "critical_notifications");

            migrationBuilder.DropTable(
                name: "cumulative_st_indicators");

            migrationBuilder.DropTable(
                name: "daily_summaries");

            migrationBuilder.DropTable(
                name: "dangers");

            migrationBuilder.DropTable(
                name: "dashboard_assets");

            migrationBuilder.DropTable(
                name: "dashboard_technicians");

            migrationBuilder.DropTable(
                name: "dashboard_tickets");

            migrationBuilder.DropTable(
                name: "generated_reports");

            migrationBuilder.DropTable(
                name: "hazards");

            migrationBuilder.DropTable(
                name: "headquarters");

            migrationBuilder.DropTable(
                name: "heat_map_zones");

            migrationBuilder.DropTable(
                name: "historical_incident_records");

            migrationBuilder.DropTable(
                name: "historical_trends");

            migrationBuilder.DropTable(
                name: "inspections");

            migrationBuilder.DropTable(
                name: "kpi_dashboards");

            migrationBuilder.DropTable(
                name: "measure_verifications");

            migrationBuilder.DropTable(
                name: "mitigations");

            migrationBuilder.DropTable(
                name: "monthly_reports");

            migrationBuilder.DropTable(
                name: "org_assets");

            migrationBuilder.DropTable(
                name: "pattern_alerts");

            migrationBuilder.DropTable(
                name: "photo_evidences");

            migrationBuilder.DropTable(
                name: "predictive_indicators");

            migrationBuilder.DropTable(
                name: "preventive_maintenances");

            migrationBuilder.DropTable(
                name: "risk_assessments");

            migrationBuilder.DropTable(
                name: "risk_patterns");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "sessions");

            migrationBuilder.DropTable(
                name: "sla_alerts");

            migrationBuilder.DropTable(
                name: "technicians");

            migrationBuilder.DropTable(
                name: "ticket_histories");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
