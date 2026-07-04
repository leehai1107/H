# PROJECT WORKFLOW

## 1. Tổng quan dự án

Đây là một solution ERP theo hướng phân lớp:

- `ERP.Desktop`: WPF UI, là điểm vào và nơi chứa shell giao diện.
- `ERP.Application`: lớp nghiệp vụ ứng dụng, hiện chưa có logic thực tế.
- `ERP.Domain`: lớp domain, hiện mới là khung.
- `ERP.Infrastructure`: lớp hạ tầng, hiện mới là khung.
- `ERP.Shared`: dùng để chia sẻ kiểu dữ liệu hoặc tiện ích chung.

## 2. Luồng chạy hiện tại

Luồng khởi động đang đi theo chuỗi sau:

1. `App.xaml` khởi động `Views/MainWindow.xaml`.
2. `MainWindow.xaml.cs` gán `DataContext = new MainWindowViewModel()`.
3. `MainWindowViewModel` khởi tạo `CurrentView = new DashboardViewModel()`.
4. `MainWindow.xaml` bind `ContentControl.Content` vào `CurrentView`.
5. `App.xaml` map `DashboardViewModel` sang `DashboardView` bằng `DataTemplate`.
6. Kết quả là dashboard được render ở vùng nội dung trung tâm.

## 3. Cấu trúc UI hiện tại

`MainWindow` đang đóng vai trò shell layout với 3 vùng chính:

- Header: `Controls/Layout/HeaderControl.xaml`
- Sidebar: `Controls/Layout/SidebarControl.xaml`
- Content: `ContentControl` bind theo `CurrentView`
- Status bar: `Controls/Layout/StatusBarControl.xaml`

Hiện tại sidebar mới chỉ là giao diện nút tĩnh, chưa có command hoặc navigation logic để đổi view.

## 4. Các màn hình đã có

Hiện có các view sau:

- `DashboardView`
- `CustomerView`
- `InventoryView`
- `ProductionView`

Trong số đó, `DashboardView` đã có nội dung hiển thị cơ bản, các view còn lại đang là khung trống.

## 5. Workflow phát triển hợp lý cho dự án

### Giai đoạn A: Shell và điều hướng

1. Chốt `MainWindow` là shell chính.
2. Bổ sung command trong `MainWindowViewModel` để đổi `CurrentView`.
3. Nối các nút sidebar vào command tương ứng.
4. Dùng `DataTemplate` để map từng ViewModel sang View.

### Giai đoạn B: Dựng từng module nghiệp vụ

1. Dashboard: dashboard tổng quan, KPI, trạng thái hệ thống.
2. Customer: danh sách khách hàng, tìm kiếm, thêm/sửa/xóa.
3. Inventory: danh mục hàng hóa, tồn kho, nhập/xuất.
4. Production: lệnh sản xuất, tiến độ, trạng thái chuyền.
5. Purchase/Settings: thêm sau khi chốt scope.

### Giai đoạn C: Tách lớp nghiệp vụ

1. Định nghĩa entity và contract trong `ERP.Domain`.
2. Viết use case/service trong `ERP.Application`.
3. Triển khai access database, file, API trong `ERP.Infrastructure`.
4. Dùng `ERP.Shared` cho DTO, enum, constants dùng chung.

### Giai đoạn D: Hoàn thiện hạ tầng kỹ thuật

1. Thiết lập dependency injection trong Desktop.
2. Thêm configuration và logging.
3. Chuẩn hóa validation, exception handling, loading state.
4. Chuẩn hóa navigation và state management.

## 6. Workflow vận hành chức năng

Luồng chuẩn cho một chức năng mới nên đi theo thứ tự:

1. Chọn module cần thêm.
2. Thiết kế entity/DTO ở `Domain` hoặc `Shared`.
3. Viết use case ở `Application`.
4. Cắm implementation ở `Infrastructure`.
5. Tạo ViewModel và View ở `Desktop`.
6. Gắn command/navigate từ sidebar hoặc từ màn hình liên quan.
7. Kiểm tra binding, template, trạng thái UI.

## 7. Nhận xét nhanh về trạng thái hiện tại

- Dự án đã có cấu trúc đa tầng rõ ràng.
- Phần UI shell đã hình thành.
- Phần điều hướng và nghiệp vụ vẫn đang ở mức khởi tạo.
- Hiện tại phù hợp nhất để tiếp tục bằng cách hoàn thiện MVVM navigation và tách service/use case.
