Changelog:

29/5/2024 (Not tested) added InternInfo create, update & delete endpoints
30/5/2024 12:58 (Not tested) merged with branch hungnt-main
1/6/2024 10:15 (Not tested) Added CRUD endpoints for "TruongHoc" & GetById endpoint for "InternInfo". Made some changes to error handling. "error" string added into response objects & is used to log errors.
1/6/2024 19:48 (Not tested) Removed InternController & moved all endpoints to InternInfoController. Tweaked Automapper to ignore null parameters while mapping objects in Update endpoints 
2/6/2024 18:17 (Not Tested) Added KyThucTap CRUD endpoints, Tweaked Validation for InternInfo Id
2/6/2024 20:26 (Not Tested) Added DuAn CRUD endpoints, tweaked Get endpoints to include error handling
3/6/2024 13:41 (Not Tested) Fixed API defintion error
3/6/2024 18:55 (Not Tested) Modified some validation conditions