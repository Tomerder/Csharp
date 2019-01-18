tic;
fileId = fopen('Matrix.txt','r');
openFileTime = toc
% Prepare the format of input file Matrix [5000x5000]
format = '%d';
for i = 1:75000000
    format = strcat(format,',',format);
    if length(format) >= 75000000
        break
    end
end
% Parse the input file into 5000x5000 Matrix
tic;
M = fscanf(fileId,format, [5000 5000]);
ParseTime = toc
MatrixDim = size(M)
% Calculate Matrix Sum
tic
MSum = sum(sum(M));
MatrixSumTime = toc
MSum