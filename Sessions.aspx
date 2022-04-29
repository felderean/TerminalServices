<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sessions.aspx.cs" Inherits="eFormsBiz.Sessions" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sessions</title>
    <style type="text/css">
        .auto-style1 {
            width: 10%;
        }
        .auto-style3 {
            width: 23%;
            height: 26px;
        }
        .auto-style4 {
            width: 10%;
            height: 26px;
        }
        .auto-style6 {
            font-size: large;
            color: #507CD1;
            font-weight: bold;
        }
        .auto-style28 {
            height: 35px;
        }
        .auto-style32 {
            height: 26px;
        }
        .auto-style33 {
            margin-left: 0px;
        }
        .auto-style34 {
            margin-right: 0px;
        }
        .auto-style35 {
            height: 26px;
            width: 258px;
        }
        .auto-style36 {
            font-size: large;
            color: #507CD1;
            font-weight: bold;
            width: 258px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="#507CD1"></asp:Label></td>
                <td align="right">
                    <asp:Label ID="lblDisplay" runat="server" Font-Bold="True" ForeColor="#507CD1"></asp:Label></td>
                <td align="right">
                    <asp:Label ID="lblUsername" runat="server" Font-Bold="True" ForeColor="#507CD1"></asp:Label>
                    <asp:LinkButton ID="btnLogout" runat="server" OnClick="btnLogout_Click">Log Out</asp:LinkButton></td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td colspan="1">
                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="Default.aspx">Return to Menu</asp:HyperLink></td>
                        </tr>
                        <tr>
                            <td colspan="1">
                                <asp:HyperLink ID="lnkIndex" runat="server" NavigateUrl="Index.aspx">Return to Index</asp:HyperLink></td>
                        </tr>
                    </table>
                </td>
                <td align="right" colspan="2">
                </td>
            </tr>
        </table>
        <hr style="width: 100%; color: #507cd1" />
                <table>
                    <tr>
                        <td class="auto-style35">
                            <asp:Label ID="lblTitle1" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#507CD1">Server Name</asp:Label>
                        </td>
                        <td class="auto-style32">
                            <asp:TextBox ID="txtServerName" runat="server" AutoCompleteType="Disabled" Width="313px"></asp:TextBox>
                        </td>
                        <td class="auto-style32">
                            </td>
                        <td class="auto-style32">
                            </td>
                        <td class="auto-style4">
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style36">
                            <asp:Label ID="lblTitle0" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#507CD1">User ID</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserID" runat="server" AutoCompleteType="Disabled" Width="313px" CssClass="auto-style33"></asp:TextBox>
                            </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td class="auto-style1">
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style36">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td class="auto-style1">
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style36">Status</td>
                        <td>
                            <asp:CheckBoxList ID="chkStatus" runat="server">
                                <asp:ListItem Selected="True">Active</asp:ListItem>
                                <asp:ListItem Selected="True">Disconnected</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td class="auto-style1">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style36">&nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td class="auto-style1">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style36">
                            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td class="auto-style1">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style36">
                            <asp:Button ID="btnReset" runat="server" Height="47px" OnClick="btnReset_Click" Text="Reset" Width="211px" UseSubmitBehavior="False" />
                        </td>
                        <td>
                            <asp:Button ID="btnFilter" runat="server" Height="47px" OnClick="btnFilter_Click" Text="Filter" Width="211px" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td class="auto-style1">
                            &nbsp;</td>
                    </tr>
                     </table>
                 <table>
                    <tr>
                        <td class="auto-style6">Data</td>
                        <td>&nbsp;</td>
                        <td class="auto-style6">&nbsp;</td>
                        <td class="auto-style1">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3">
                            <asp:GridView ID="gvSessions" runat="server" DataSourceID="dsSessions" AutoGenerateColumns="False" HorizontalAlign="Left" PageSize="40" Width="146%" CssClass="auto-style34" AllowPaging="True" AllowSorting="True" >
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:HyperLinkField DataTextField="LogOff" NavigateUrl="~/LogOff.aspx" Text="Log Off" />
                                    <asp:BoundField DataField="UserName" HeaderText="User ID" SortExpression="UserName" />
                                    <asp:BoundField DataField="ConnectionState" HeaderText="Status" SortExpression="ConnectionState" />
                                    <asp:BoundField DataField="StationName" HeaderText="Workstation Name" SortExpression="StationName" />
                                    <asp:BoundField DataField="SessionID" HeaderText="Session ID" SortExpression="SessionID" />
                                </Columns>
                                <EmptyDataRowStyle BackColor="#507CD1" ForeColor="White" />
                                <EmptyDataTemplate>
                                    No Matching Entries
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#507CD1" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                             </asp:GridView>
                        </td>
                        <td class="auto-style3">
                            &nbsp;</td>
                        <td class="auto-style3">
                            &nbsp;</td>
                        <td class="auto-style4">
                            </td>
                    </tr>
                </table>

                <br />
                <table style="width:100%;">
                    <tr>
                        <td class="auto-style28">
                            <asp:ObjectDataSource ID="dsSessions" runat="server" TypeName="eFormsBiz.CSessionManager" SelectMethod="GetSessions" >
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtServerName" Name="pServerName" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
       </form>
    </body>
</html>

