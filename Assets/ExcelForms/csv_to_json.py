import pandas as pd
import json
import os
import ast

# 获取脚本所在目录
script_dir = os.path.dirname(os.path.abspath(__file__))

# 配置输入和输出文件名
input_csv = 'IntentJsonConfig.csv'
output_json = input_csv.replace('.csv', '.json')

# 构建路径
csv_path = os.path.join(script_dir, input_csv)
json_path = os.path.join(script_dir, output_json)

# 自动解析字符串形式的 JSON 数组或对象
def parse_json_string(s):
    if not isinstance(s, str):
        return s
    s = s.strip()
    try:
        if (s.startswith('{') and s.endswith('}')) or (s.startswith('[') and s.endswith(']')):
            return ast.literal_eval(s)
        return s
    except (ValueError, SyntaxError):
        return s

# 读取 CSV
try:
    df = pd.read_csv(csv_path, encoding='utf-8')
except Exception as e:
    print(f"读取 CSV 失败: {e}")
    exit(1)

# 自动识别并解析所有字段中可能是 JSON 字符串的内容
for column in df.columns:
    df[column] = df[column].apply(parse_json_string)

# 检查 NaN 值
nan_locations = df.isna()
if nan_locations.any().any():
    print("警告：CSV 中存在空值（NaN）！以下是详细信息：")
    for col in df.columns:
        if nan_locations[col].any():
            rows = df[nan_locations[col]].index.tolist()
            print(f"列 '{col}' 在以下行有空值: {rows}")

# 替换 NaN 为 None
df = df.where(df.notna(), None)

# 转换为 JSON
data = {input_csv.replace('.csv', ''): df.to_dict(orient='records')}

# 保存 JSON
try:
    with open(json_path, 'w', encoding='utf-8') as f:
        json.dump(data, f, indent=4, ensure_ascii=False)
    print(f"转换成功！生成文件: {json_path}")
except Exception as e:
    print(f"保存 JSON 失败: {e}")
    exit(1)
